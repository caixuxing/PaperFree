using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal class FindProcessNodePageListHandler : IRequestHandler<FindProcessNodePageListQry, PageData<List<ProcessNodePageListDto>>>
    {
        private readonly IProcessNodeRepo processNodeRepo;

        public FindProcessNodePageListHandler(IProcessNodeRepo processNodeRepo)
        {
            this.processNodeRepo = processNodeRepo;
        }

        public async Task<PageData<List<ProcessNodePageListDto>>> Handle(FindProcessNodePageListQry request, CancellationToken cancellationToken)
        {
            var query = processNodeRepo.QueryAll().AsNoTracking()
                 .Include(x => x.ProcessDesign)
                 .Include(x => x.NodeApprovers)
                 .Select(x => new ProcessNodePageListDto()
                 {
                     Id = x.Id,
                     ProcessDesignId = x.ProcessDesignId,
                     ProcessDesignName = x.ProcessDesign.ProcessName,
                     ProcessTempType = x.ProcessDesign.ProcessTempType,
                     NodeName = x.NodeName,
                     NodeCode = x.NodeCode,
                     UpperNodeId = x.UpperNodeId,
                     LowerNodeId = x.LowerNodeId,
                     OderNo = x.OderNo,
                     CreateTime = x.CreationTime,
                     CurrentNodeApprovers = string.Join(",", x.NodeApprovers.Select(x => $"{x.ApproverAccount}【{x.ApproverName}】").ToList()),
                 })
                 .Where(x => x.ProcessDesignId == request.ProcessDesignId)
                 .OrderBy(x => x.OderNo);
            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            data.ForEach(item =>
            {
                item.UpperNodeName = data.FirstOrDefault(s => s.Id == item.UpperNodeId)?.NodeName ?? "开始";
                item.LowerNodeName = data.FirstOrDefault(s => s.Id == item.LowerNodeId)?.NodeName ?? "结束";
                if (new List<ProcessTempType>(){ ProcessTempType.ARCHIVE, ProcessTempType.RECALL }.Contains(item.ProcessTempType))
                {
                    item.NodeMapWorkflowStatusName = ((WorkFlowState)Enum.Parse(typeof(WorkFlowState), item.NodeMapWorkflowStatus.ToString())).ToDescription();
                }
            });
            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}