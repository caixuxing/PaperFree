using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal class FindProcessNodeByIdHandler : IRequestHandler<FindProcessNodeByIdQry, ProcessNodeDetailDto?>
    {
        private readonly IProcessNodeRepo processNodeRepo;
        private readonly IMapper mapper;

        public FindProcessNodeByIdHandler(IProcessNodeRepo processNodeRepo, IMapper mapper)
        {
            this.processNodeRepo = processNodeRepo;
            this.mapper = mapper;
        }

        public async Task<ProcessNodeDetailDto?> Handle(FindProcessNodeByIdQry request, CancellationToken cancellationToken)
        {
            var data = await processNodeRepo.FindById(request.Id);
            if (data is not null)
                return new ProcessNodeDetailDto()
                {
                    ID = data.Id,
                    NodeName = data.NodeName,
                    NodeCode = data.NodeCode,
                    UpperNodeId = data.UpperNodeId,
                    LowerNodeId = data.LowerNodeId,
                    EventDirectionBranch = data.EventDirectionBranch.Split(",").Select(x => x.ToEnum<EventDirectionType>()).ToList(),
                    NodeMapWorkflowStatus = data.NodeMapWorkflowStatus,
                    IsRejectToNode = data.IsRejectToNode,
                    NodeApprovers = data.NodeApprovers.Select(x => new NodeApproverValueObj() { ApproverId = x.ApproverId, ApproverAccount = x.ApproverAccount, ApproverName = x.ApproverName }).ToList(),
                    OderNo = data.OderNo,
                    ProcessDesignId = data.ProcessDesignId
                };
            return null;
        }
    }
}