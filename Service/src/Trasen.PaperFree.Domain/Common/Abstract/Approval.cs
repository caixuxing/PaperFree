using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;

namespace Trasen.PaperFree.Domain.Common.Abstract
{
    /// <summary>
    /// 审批状态流程控制
    /// </summary>
    public abstract class Approval<T>
    {
        /// <summary>
        ///审批状态事件
        /// </summary>
        /// <param name="archiveApply"></param>
        /// <param name="ProcessDesign"></param>
        /// <returns></returns>
        public abstract WorkFlowState ApprovalResult(T Apply, ProcessDesign ProcessDesign);

        /// <summary>
        /// 是否驳回指定节点
        /// </summary>
        public bool IsRejectToNode { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string RejectNodeId { get; set; } = string.Empty;
    }
}