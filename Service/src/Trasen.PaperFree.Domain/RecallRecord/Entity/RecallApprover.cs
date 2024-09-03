using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.RecallRecord.Entity
{
    /// <summary>
    /// 归档审批记录表
    /// </summary>
    public record RecallApprover : FullRoot
    {
        public RecallApprover(
            string id,
            string recallApplyId,
            WorkFlowState workFlowStatus,
            EventDirectionType approvalResult,
            string? approvalRemark,
            string approvalId, 
            DateTime approvalDateTime)
        {
            Id = id;
            RecallApplyId = recallApplyId;
            WorkFlowStatus = workFlowStatus;
            ApprovalResult = approvalResult;
            ApprovalRemark = approvalRemark;
            ApprovalId = approvalId;
            ApprovalDateTime = approvalDateTime;
        }

        private RecallApprover() { }
        /// <summary>
        /// 召回申请ID
        /// </summary>
        public string RecallApplyId { get; private set; }
        /// <summary>
        /// 召回申请实体
        /// </summary>
        public virtual RecallApply RecallApply { get; private set; }
        /// <summary>
        /// 状态
        /// </summary>
        public WorkFlowState WorkFlowStatus { get; set; }

        /// <summary>
        /// 审批结果 审批结果：通过、拒绝、驳回
        /// </summary>
        public EventDirectionType ApprovalResult { get; private set; }
        /// <summary>
        /// 审批备注
        /// </summary>
        public string? ApprovalRemark { get; private set; }
        /// <summary>
        /// 审批人ID
        /// </summary>
        public string ApprovalId { get; private set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ApprovalDateTime { get; private set; }
    }
}