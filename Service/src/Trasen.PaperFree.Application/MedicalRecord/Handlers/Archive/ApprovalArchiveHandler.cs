using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.MedicalRecord.Commands.Archive;
using Trasen.PaperFree.Domain.ArchiveRecord.DomainService;
using Trasen.PaperFree.Domain.ArchiveRecord.Entity;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Archive
{
    internal class ApprovalArchiveHandler : IRequestHandler<ApprovalArchiveCmd, bool>
    {
        private readonly Validate<ApprovalArchiveCmd> validate;
        private readonly ICurrentUser currentUser;
        private readonly IArchiveApproverRepo archiveApproverRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IArchiveApplyRepo archiveApplyRepo;
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly IArchiveRecordService archiveRecordService;
        private readonly IOutpatientInfoRepo outpatientInfoRepo;

        public ApprovalArchiveHandler(Validate<ApprovalArchiveCmd> validate, ICurrentUser currentUser,
            IArchiveApproverRepo archiveApproverRepo, IUnitOfWork unitOfWork, IArchiveApplyRepo archiveApplyRepo,
            IProcessDesignRepo processDesignRepo, IArchiveRecordService archiveRecordService,
            IOutpatientInfoRepo outpatientInfoRepo)
        {
            this.validate = validate;
            this.currentUser = currentUser;
            this.archiveApproverRepo = archiveApproverRepo;
            this.unitOfWork = unitOfWork;
            this.archiveApplyRepo = archiveApplyRepo;
            this.processDesignRepo = processDesignRepo;
            this.archiveRecordService = archiveRecordService;
            this.outpatientInfoRepo = outpatientInfoRepo;
        }

        public async Task<bool> Handle(ApprovalArchiveCmd request, CancellationToken cancellationToken)
        {
            //校验请求参数
            await validate.ValidateAsync(request);

            //申请归档信息
            var archiveApply = await archiveApplyRepo.FindById(request.ArchiveApplyId);
            if (archiveApply is null)
                throw new BusinessException(MessageType.Warn, "非法归档申请信息,审批失败！");
            if (archiveApply.CurrentStatus == ProcessStatusType.END)
                throw new BusinessException(MessageType.Warn, "流程已结束,请勿重复审批！");

            //流程模板信息
            var processDesign = await processDesignRepo.QueryAll().AsTracking()
                .Include(x => x.ProcessNodes)
                .ThenInclude(x=>x.NodeApprovers)
                .SingleOrDefaultAsync(x => x.Id == archiveApply.ProcessDesignId);

            if (processDesign is null)
                throw new BusinessException(MessageType.Warn, "流程模板缺失无法进行审批操作!");
            //流程事件状态流
            var medicalRecordStatus = archiveRecordService.ProcessEnventStatuFlow(
                request.IsRejectToNode ?? false,
                request.RejectNodeId ?? string.Empty,
                request.ApprovalResult,
                processDesign,
                archiveApply);
            //更新归档申请
            archiveApplyRepo.Update(archiveApply);

            //出院信息
            var outpatientInfo = await outpatientInfoRepo.FindById(archiveApply.ArchiveId);
            if (outpatientInfo is null)
                throw new BusinessException(MessageType.Warn, "病历信息缺失无法进行审批操作!");
            //出院信息状态更改
            outpatientInfo.ChnageStatus(medicalRecordStatus);
            outpatientInfoRepo.Update(outpatientInfo);

            //创建审批信息
            var entity = new ArchiveApprover(
                request.ArchiveApplyId,
                request.ApprovalResult,
                request.ApprovalRemark,
                currentUser.Id,
                DateTime.Now,
                medicalRecordStatus
                );
            await archiveApproverRepo.AddAsync(entity, cancellationToken);

            //保存
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}