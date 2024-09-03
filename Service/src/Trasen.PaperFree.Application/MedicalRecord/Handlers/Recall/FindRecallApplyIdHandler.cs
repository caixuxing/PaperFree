using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.Dto;
using Trasen.PaperFree.Application.MedicalRecord.Dto.Recall;
using Trasen.PaperFree.Application.MedicalRecord.Query.Recall;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.RecallRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Recall
{
    /// <summary>
    /// 召回审批详细
    /// </summary>
    internal class FindRecallApplyIdHandler : IRequestHandler<FindRecallApplyIdQry, RecallApplyDetailDto>
    {
        private readonly IRecallApplyRepo recallApplyRepo;
        private readonly IProcessNodeRepo processNodeRepo;

        public FindRecallApplyIdHandler(IRecallApplyRepo recallApplyRepo, IProcessNodeRepo processNodeRepo)
        {
            this.recallApplyRepo = recallApplyRepo;
            this.processNodeRepo = processNodeRepo;
        }

        public async Task<RecallApplyDetailDto> Handle(FindRecallApplyIdQry request, CancellationToken cancellationToken)
        {
            var model = await recallApplyRepo.QueryAll().AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (model is null) throw new BusinessException(MessageType.Warn, "非法流程信息");
            if (model.IsEnd) throw new BusinessException(MessageType.Warn, "流程已结束,无法加载审批信息");
            var nodeList = await processNodeRepo.QueryAll().AsNoTracking().Where(x => x.ProcessDesignId == model.ProcessDesignId).ToListAsync();
            var nodelModel = nodeList.FirstOrDefault(x => x.Id == model.CurrentApprovalNodeId);
            if (nodelModel is null) throw new BusinessException(MessageType.Warn, "未找到当前流程节点审批配置信息");
            List<DropSelectDto<int>> eventDirectionTypes = new();
            nodelModel.EventDirectionBranch.Split(",").ToList().ForEach(item =>
            {
                var EventDirectionType = (EventDirectionType)Enum.Parse(typeof(EventDirectionType), item.ToString(), true);
                eventDirectionTypes.Add(new DropSelectDto<int>() { Id = EventDirectionType.GetHashCode(), Name = EventDirectionType.ToDescription() });
            });
            return new RecallApplyDetailDto
            {
                NodeName = nodelModel.NodeName,
                IsRejectToNode = nodelModel.IsRejectToNode,
                NodeList = (nodelModel.IsRejectToNode??false)
                ?nodeList.Where(x => x.Id != model.CurrentApprovalNodeId)
                .Select(x => new DropSelectDto<string>() { Id = x.Id.ToString(), Name = x.NodeName }).ToList()
                :new(),
                UpperNodeId = nodelModel.UpperNodeId,
                eventDirectionTypes = eventDirectionTypes
            };
        }
    }
}