using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.MedicalRecord.Dto.Archive;
using Trasen.PaperFree.Application.MedicalRecord.Query.Archive;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Archive
{
    internal sealed class FindApprovalTimelineHandler : IRequestHandler<FindApprovalTimelineQry, ApprovalTimelineDto>
    {
        /// <summary>
        /// 出院信息仓储
        /// </summary>
        private readonly IOutpatientInfoRepo outpatientInfoRepo;
        /// <summary>
        /// 召回申请仓储
        /// </summary>
        private readonly IArchiveApplyRepo archiveApplyRepo;

        public FindApprovalTimelineHandler(IOutpatientInfoRepo outpatientInfoRepo,
            IArchiveApplyRepo archiveApplyRepo)
        {
            this.outpatientInfoRepo = outpatientInfoRepo;
            this.archiveApplyRepo = archiveApplyRepo;
        }

        public async Task<ApprovalTimelineDto> Handle(FindApprovalTimelineQry request, CancellationToken cancellationToken)
        {
            ApprovalTimelineDto listData = new();
            var entity = await archiveApplyRepo.QueryAll().AsNoTracking().Where(x => x.Id == request.ArchiveApplyId)
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
                    listData.approvalStatusFlows.Add(new ApprovalStatusFlowValueObj() { StatusName = item.WorkFlowStatus.ToDescription(), DateTime = item.CreationTime });
            }
            return listData;
        }
    }
}