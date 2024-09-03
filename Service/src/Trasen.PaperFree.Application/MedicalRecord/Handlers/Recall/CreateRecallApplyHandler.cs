using DapperExtensions.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Trasen.PaperFree.Application.MedicalRecord.Commands.Recall;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.RecallRecord.Entity;
using Trasen.PaperFree.Domain.RecallRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Recall
{
    /// <summary>
    /// 召回申请处理器
    /// </summary>
    internal sealed class CreateRecallApplyHandler : IRequestHandler<CreateRecallApplyCmd, bool>
    {
        private readonly IRecallApplyRepo recallApplyRepo;
        private readonly IOutpatientInfoRepo outpatientInfoRepo;
        private readonly Validate<CreateRecallApplyCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly ICurrentUser currentUser;
        private readonly IGuidGenerator guidGenerator;

        public CreateRecallApplyHandler(
            IRecallApplyRepo recallApplyRepo,
            IOutpatientInfoRepo outpatientInfoRepo,
            Validate<CreateRecallApplyCmd> validate,
            IUnitOfWork unitOfWork,
            IProcessDesignRepo processDesignRepo,
            ICurrentUser currentUser,
            IGuidGenerator guidGenerator)
        {
            this.recallApplyRepo = recallApplyRepo;
            this.outpatientInfoRepo = outpatientInfoRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.processDesignRepo = processDesignRepo;
            this.currentUser = currentUser;
            this.guidGenerator = guidGenerator;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Handle(CreateRecallApplyCmd request, CancellationToken cancellationToken)
        {
            //验证入参
            await validate.ValidateAsync(request);
            //读取病历信息
            var outpatientInfo = await outpatientInfoRepo.FindById(request.ArchivalId);
            if (outpatientInfo is null)
                throw new BusinessException(MessageType.Warn, "非法病历信息无法执行召回,操作失败！");



            if (outpatientInfo.Status != WorkFlowState.ALREADYARCHIVE)
                throw new BusinessException(MessageType.Warn, $"召回病历非{WorkFlowState.ALREADYARCHIVE.Description()}状态病历,操作失败！");


            //验证当前归档流程是否结束
            if (await recallApplyRepo.QueryAll().AsNoTracking().AnyAsync(x => x.ArchiveId == request.ArchivalId && x.IsEnd == false))
                throw new BusinessException(MessageType.Warn, $"召回【{outpatientInfo.Name}:{outpatientInfo.AdmissId}】流程未结束,请勿重复提交申请");


            //读取流程模板
            var model = await processDesignRepo.QueryAll().AsNoTracking()
                .Include(x => x.ProcessNodes.OrderBy(x => x.OderNo))
                .ThenInclude(node => node.NodeApprovers)
                .FirstOrDefaultAsync(x => x.IsEnable == true &&
                                            x.DeptCode == outpatientInfo.OutDeptCode &&
                                            x.ProcessTempType == ProcessTempType.RECALL &&
                                            x.OrgCode == outpatientInfo.OrgCode &&
                                            x.HospCode == outpatientInfo.HospCode);
            if (model is null || !model.ProcessNodes.Any())
                throw new BusinessException(MessageType.Warn, "没有找到流程模板,请先设置召回流程模板！");
            var nodeModel = model.ProcessNodes.OrderBy(x => x.OderNo).FirstOrDefault();
            var RecallApplyId = guidGenerator.Create().ToString();
            //创建归档申请
            var entity = new RecallApply(
                RecallApplyId,
                request.ArchivalId,
                $"召回【{outpatientInfo.Name}:{outpatientInfo.AdmissId}】",
                $"【{outpatientInfo.Name}:{outpatientInfo.AdmissId}】召回申请",
                string.Empty,
                false,
                model.Id,
                nodeModel!.Id,
                nodeModel.NodeName,
                ProcessStatusType.AWAITAPPROVAL,
                string.Join(",", nodeModel.NodeApprovers.Select(x => $"{x.ApproverAccount}【{x.ApproverName}】")),
                currentUser.UserNickName,
                outpatientInfo.OrgCode, outpatientInfo.HospCode ?? string.Empty);
            await recallApplyRepo.AddAsync(entity, cancellationToken);
            //改变病历状态已提交
            outpatientInfo.ChnageStatus(WorkFlowState.AWAITCOMMIT);
            outpatientInfoRepo.Update(outpatientInfo);
            //保存
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}