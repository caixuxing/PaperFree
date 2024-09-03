using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord.ValueObj;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal sealed class FindProcessDesignByIdHandler : IRequestHandler<FindProcessDesignByIdQry, ProcessDesignDetaiDto?>
    {
        private readonly IProcessDesignRepo processDesignRepo;

        public FindProcessDesignByIdHandler(IProcessDesignRepo processDesignRepo)
        {
            this.processDesignRepo = processDesignRepo;
        }

        public async Task<ProcessDesignDetaiDto?> Handle(FindProcessDesignByIdQry request, CancellationToken cancellationToken)
        {
            var data = await processDesignRepo.QueryAll().AsNoTracking()
                .Include(x => x.ProcessNodes)
                .ThenInclude(x => x.NodeApprovers)
                .Select(_ => new
                {
                   _.Id, _.IsEnable,_.ProcessName,_.ProcessTempType,_.DeptCode,_.OrgCode, _.HospCode,
                    workNodes = _.ProcessNodes
                    .Select(x => new
                    {
                        x.EventDirectionBranch,
                        x.Id,
                        x.OderNo,
                        x.IsRejectToNode,
                        x.NodeName,
                        x.NodeMapWorkflowStatus,
                        NodeApprovers = x.NodeApprovers.Select(q => new { q.ApproverAccount, q.ApproverId, q.ApproverName }).ToList()
                    }).OrderBy(x=>x.OderNo).ToList(),
                }).FirstOrDefaultAsync(_ => _.Id == request.Id);
            if (data is null) return null;
            return new ProcessDesignDetaiDto()
            {
                Id = data.Id,
                DeptCode = data.DeptCode,
                HospCode = data.HospCode,
                IsEnable = data.IsEnable,
                ProcessName = data.ProcessName,
                OrgCode = data.OrgCode,
                processTempType = data.ProcessTempType,
                workNodes = data.workNodes.Select(x => new WorkNode()
                {
                    Id = x.Id,
                    IsRejectToNode = x.IsRejectToNode,
                    NodeName = x.NodeName,
                    NodeMapWorkflowStatus = x.NodeMapWorkflowStatus,
                    EventDirectionBranch = x.EventDirectionBranch.Split(",").Select(x => x.ToEnum<EventDirectionType>()).ToList(),
                    CurrentNodeApprovers = x.NodeApprovers.Select(x => new NodeApprover() { ApproverId = x.ApproverId, ApproverAccount = x.ApproverAccount, ApproverName = x.ApproverName }).ToList(),
                }).ToList()
            };
        }
    }
}