using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.MedicalRecord.Commands.Recall;
using Trasen.PaperFree.Domain.MedicalRecord.Repository;
using Trasen.PaperFree.Domain.RecallRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Recall
{
    /// <summary>
    /// 删除召回申请处理器
    /// </summary>
    internal class DeleteRecallApplyHandler : IRequestHandler<DeleteRecallApplyCmd, bool>
    {
        private readonly IRecallApplyRepo recallApplyRepo;
        readonly IOutpatientInfoRepo outpatientInfoRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteRecallApplyHandler(IRecallApplyRepo recallApplyRepo, IUnitOfWork unitOfWork, IOutpatientInfoRepo outpatientInfoRepo)
        {
            this.recallApplyRepo = recallApplyRepo;
            this.unitOfWork = unitOfWork;
            this.outpatientInfoRepo = outpatientInfoRepo;
        }

        public async Task<bool> Handle(DeleteRecallApplyCmd request, CancellationToken cancellationToken)
        {
            var list = await recallApplyRepo.QueryAll().Where(x => x.Id == request.id)
                 .Include(x => x.RecallApplyAndArchivals).ToListAsync();
            if (list is null || !list.Any()) throw new BusinessException(MessageType.Error, "删除失败", "实体信息不存在,无法执行删除！");
            var recallApplyModel = list.FirstOrDefault();
            if (recallApplyModel!.CurrentStatus != ProcessStatusType.AWAITAPPROVAL)
                throw new BusinessException(MessageType.Warn, $"流程审批状态非【{ProcessStatusType.AWAITAPPROVAL.ToDescription()}】不允许删除！");
            recallApplyModel!.ChangeDelete();
            recallApplyModel.RecallApplyAndArchivals.ToList().ForEach(item => item.ChangeDelete());
            recallApplyRepo.Update(recallApplyModel);
           var outpatientInfo= await outpatientInfoRepo.FindById(list.FirstOrDefault()!.ArchiveId);
            if (outpatientInfo is not null)
            {
                outpatientInfo.ChnageStatus(WorkFlowState.ALREADYARCHIVE);
                outpatientInfoRepo.Update(outpatientInfo);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}