using Trasen.PaperFree.Domain.ArchiveRecord.Entity;
using Trasen.PaperFree.Domain.Common.Abstract;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Const;
using Trasen.PaperFree.Domain.Shared.CustomException;
using Trasen.PaperFree.Domain.Shared.Enums;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Domain.ArchiveRecord.DomainService.Impl
{
    /// <summary>
    ///
    /// </summary>
    internal class ArchiveRecordService : IArchiveRecordService
    {
        private static readonly Lazy<Dictionary<EventDirectionType, Approval<ArchiveApply>>> Init =
            new Lazy<Dictionary<EventDirectionType, Approval<ArchiveApply>>>(() =>
            {
                return new Dictionary<EventDirectionType, Approval<ArchiveApply>>(){
            { EventDirectionType.END,new End() },
            { EventDirectionType.REJECT,new Reject()},
            { EventDirectionType.PASS,new Pass()} };
            });

        public WorkFlowState ProcessEnventStatuFlow(bool IsRejectToNode, string RejectNodeId, EventDirectionType eventDirectionType, ProcessDesign processDesign, ArchiveApply archiveApply)
        {
            var approval = Init.Value.Where(t => t.Key.Equals(eventDirectionType)).Select(x => x.Value).FirstOrDefault();
            if (approval is null)
                throw new BusinessException(MessageType.Error, "未找到审批结果处理事件,无法流转病历状态！", "未找到审批结果处理事件,无法流转病历状态！");
            approval.IsRejectToNode = IsRejectToNode;
            approval.RejectNodeId = RejectNodeId;
            var result = approval.ApprovalResult(archiveApply, processDesign);
            return result;
        }

        /// <summary>
        /// 归档-通过
        /// </summary>
        private class Pass : Approval<ArchiveApply>
        {
            public override WorkFlowState ApprovalResult(ArchiveApply archiveApply, ProcessDesign processDesign)
            {
                //获取当前节点信息
                var nodeInfo = processDesign.ProcessNodes.FirstOrDefault(n => n.Id == archiveApply.CurrentApprovalNodeId);
                if (nodeInfo is null)
                    throw new BusinessException(MessageType.Warn, "找不到对应流程节点信息,审批失败！");
                //节点审批人员
                string nodeApprovaName=string.Empty;
                if (nodeInfo.LowerNodeId != CustomConstant.EndGuId)
                {
                    nodeApprovaName = string.Join(",", processDesign.ProcessNodes.FirstOrDefault(n => n.Id == nodeInfo.LowerNodeId)?.NodeApprovers
                        .Select(x => $"{x.ApproverAccount}【{x.ApproverName}】").ToList()!);
                    if (string.IsNullOrWhiteSpace(nodeApprovaName))
                        throw new BusinessException(MessageType.Warn, "找不到下级节点审批人员,审批失败");
                }
                //更新归档申请当前审批节点
                archiveApply.SetCurrentApprovalNodeId(nodeInfo.LowerNodeId)
                    .SetCurrentStatus(ProcessStatusType.UNDERAPPROVAL)
                    .SetNodeApprovaName(nodeApprovaName);
                //当前行在集合中的索引位置
                var currentRowIndex = processDesign.ProcessNodes.OrderBy(x => x.OderNo).ToList().IndexOf(nodeInfo!) + 1;
                //判断流程节点是否为最后节点
                if (currentRowIndex == processDesign.ProcessNodes.Count)
                {
                    //更新归档申请状态
                    archiveApply.SetCurrentStatus(ProcessStatusType.FINISH)
                        .SetIsEnd(true)
                        .SetCurrentApprovalNodeId(null)
                        .SetNodeApprovaName(null);
                }
                return (WorkFlowState)nodeInfo.NodeMapWorkflowStatus;
            }
        }

        /// <summary>
        /// 归档-驳回
        /// </summary>
        private class Reject : Approval<ArchiveApply>
        {
            public override WorkFlowState ApprovalResult(ArchiveApply archiveApply, ProcessDesign processDesign)
            {
                //获取当前节点信息
                var nodeInfo = processDesign.ProcessNodes.FirstOrDefault(n => n.Id == archiveApply.CurrentApprovalNodeId);
                if (nodeInfo is null)
                    throw new BusinessException(MessageType.Warn, "找不到对应流程节点信息,审批驳回失败！");
                if (nodeInfo.UpperNodeId == Guid.Empty.ToString())
                    throw new BusinessException(MessageType.Warn, "已是最顶层节点了,无法继续驳回！");
                //节点审批人员
                string nodeApprovaName = string.Empty;
                if (nodeInfo.UpperNodeId != Guid.Empty.ToString())
                {
                    nodeApprovaName = string.Join(",", processDesign.ProcessNodes.FirstOrDefault(n => n.Id == nodeInfo.UpperNodeId)?.NodeApprovers
                        .Select(x => $"{x.ApproverAccount}【{x.ApproverName}】").ToList()!);
                    if (string.IsNullOrWhiteSpace(nodeApprovaName))
                        throw new BusinessException(MessageType.Warn, "找不到上级节点审批人员,审批失败");
                }
                //驳回
                archiveApply.SetCurrentStatus(ProcessStatusType.UNDERAPPROVAL)
                    .SetNodeApprovaName(nodeApprovaName)
                    .SetCurrentApprovalNodeId((this.IsRejectToNode&&!string.IsNullOrWhiteSpace(this.RejectNodeId))
                    ? this.RejectNodeId : nodeInfo.UpperNodeId);
                return (WorkFlowState)nodeInfo.NodeMapWorkflowStatus;
            }
        }

        /// <summary>
        /// 归档-拒绝(结束)
        /// </summary>
        private class End : Approval<ArchiveApply>
        {
            public override WorkFlowState ApprovalResult(ArchiveApply archiveApply, ProcessDesign processDesign)
            {
                //更新归档申请状态
                archiveApply.SetCurrentStatus(ProcessStatusType.END)
                    .SetIsEnd(true) //设置当前申请归档已结束
                    .SetCurrentApprovalNodeId(null)
                    .SetNodeApprovaName(null);
                return WorkFlowState.AWAITCOMMIT;
            }
        }
    }
}