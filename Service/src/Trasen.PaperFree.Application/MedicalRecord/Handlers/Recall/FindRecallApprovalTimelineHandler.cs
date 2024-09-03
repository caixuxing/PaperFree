using DapperExtensions.Extensions;
using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.MedicalRecord.Dto.Archive;
using Trasen.PaperFree.Application.MedicalRecord.Dto.Recall;
using Trasen.PaperFree.Application.MedicalRecord.Query.Recall;
using Trasen.PaperFree.Domain.RecallRecord.Repository;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Recall
{
    internal class FindRecallApprovalTimelineHandler : IRequestHandler<FindRecallApprovalTimelineQry, RecallApprovalTimelineDto>
    {
        /// <summary>
        /// 出院信息仓储
        /// </summary>
        private readonly IOutpatientInfoRepo outpatientInfoRepo;

        /// <summary>
        /// 召回申请仓储
        /// </summary>
        private readonly IRecallApplyRepo recallApplyRepo;

        public FindRecallApprovalTimelineHandler(IOutpatientInfoRepo outpatientInfoRepo,
            IRecallApplyRepo recallApplyRepo)
        {
            this.outpatientInfoRepo = outpatientInfoRepo;
            this.recallApplyRepo = recallApplyRepo;
        }

        /// <summary>
        /// 召回审批时间轴
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RecallApprovalTimelineDto> Handle(FindRecallApprovalTimelineQry request, CancellationToken cancellationToken)
        {
            RecallApprovalTimelineDto listData = new();
            var entity = await recallApplyRepo.QueryAll().AsNoTracking().Where(x => x.Id == request.RecallApplyId)
                .Include(x => x.ArchiveApprovers).ToListAsync();
            if (entity is not null && entity.Any())
            {
                var model = await outpatientInfoRepo.QueryAll().AsNoTracking()
                       .Select(x => new { x.ArchiveId, x.OutDate, x.EnterDate, x.AdmissId, x.Name })
                       .FirstOrDefaultAsync(x => x.ArchiveId == entity[0].ArchiveId);
                if (model is not null)
                {
                    listData.AdmissId = model.AdmissId;
                    listData.Name = model.Name;
                    listData.approvalStatusFlows.Insert(0, new ApprovalStatusFlowValueObj() { StatusName = "入院", DateTime = model.EnterDate });
                    listData.approvalStatusFlows.Insert(1, new ApprovalStatusFlowValueObj() { StatusName = "出院院", DateTime = model.EnterDate });
                }
                foreach (var item in entity[0].ArchiveApprovers)
                    listData.approvalStatusFlows.Add(new ApprovalStatusFlowValueObj() { StatusName = item.WorkFlowStatus.Description(), DateTime = item.CreationTime });
            }
            return listData;
        }
    }
}