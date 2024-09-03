using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.ArchiveRecord.Entity
{
    /// <summary>
    /// 归档审批记录表
    /// </summary>
    public record ArchiveApprover : FullRoot
    {
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="archiveApplyId">归档申请ID</param>
        /// <param name="approvalResult"> 审批结果</param>
        /// <param name="approvalRemark"></param>
        /// <param name="approvalId"></param>
        /// <param name="approvalDateTime"></param>
        public ArchiveApprover(string archiveApplyId, EventDirectionType approvalResult, string? approvalRemark,
            string approvalId, DateTime approvalDateTime, WorkFlowState workFlowStatus)
        {
            ArchiveApplyId = archiveApplyId;
            ApprovalResult = approvalResult;
            ApprovalRemark = approvalRemark;
            ApprovalId = approvalId;
            this.approvalDateTime = approvalDateTime;
            WorkFlowStatus = workFlowStatus;
        }

        private ArchiveApprover() { }

        /// <summary>
        /// 归档申请ID
        /// </summary>
        public string ArchiveApplyId { get; private set; }

        /// <summary>
        /// 归档申请实体
        /// </summary>
        public virtual ArchiveApply ArchiveApply { get; private set; }

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
        public DateTime approvalDateTime { get; private set; }
    }
}