using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.CustomException;
using Trasen.PaperFree.Domain.Shared.Enums;

namespace Trasen.PaperFree.Domain.RecallRecord.Entity
{
    /// <summary>
    /// 召回申请
    /// </summary>
    public record RecallApply : FullRoot
    {
        public RecallApply(string id,string ArchiveId, string applyName, string applyReason, string? attachmentMaterials, bool isEnd, string processDesignId,
            string? currentApprovalNodeId, string? currentApprovalNodeName, ProcessStatusType currentStatus, string? nodeApprovaName,
            string applyPersonName, string orgCode, string hospCode)
        {
            Id = id;
            this.ArchiveId = ArchiveId;
            ApplyName = applyName;
            ApplyReason = applyReason;
            AttachmentMaterials = attachmentMaterials;
            IsEnd = isEnd;
            ProcessDesignId = processDesignId;
            CurrentApprovalNodeId = currentApprovalNodeId;
            CurrentApprovalNodeName = currentApprovalNodeName;
            CurrentStatus = currentStatus;
            NodeApprovalName = nodeApprovaName;
            ApplyPersonName = applyPersonName;
            OrgCode = orgCode;
            if (string.IsNullOrWhiteSpace(hospCode))
                throw new BusinessException(Shared.Response.MessageType.Warn, "院区编码不能为空！");
            HospCode = hospCode;
        }

        private RecallApply() { }

    



        /// <summary>
        /// 病历档案号
        /// </summary>
        public string ArchiveId { get; private set; }
        /// <summary>
        /// 申请名称 默认为当前病案人归档作为名称，批量为批量归档申请
        /// </summary>
        public string? ApplyName { get; private set; }
        /// <summary>
        /// 申请原因
        /// </summary>
        public string? ApplyReason { get; private set; }
        /// <summary>
        /// 附件材料
        /// </summary>
        public string? AttachmentMaterials { get; private set; }

        /// <summary>
        /// 流程是否结束 状态：是 、否
        /// </summary>
        public bool IsEnd { get; private set; }
        /// <summary>
        /// 流程模板ID 绑定当前当前申请审批模板
        /// </summary>
        public string ProcessDesignId { get; private set; }
        /// <summary>
        /// 当前审批节点ID 当前审批节点ID
        /// </summary>
        public string? CurrentApprovalNodeId { get; private set; }
        /// <summary>
        /// 当前审批节点名称
        /// </summary>
        public string? CurrentApprovalNodeName { get; private set; }

        /// <summary>
        /// 状态 流程状态：开始》审批中》结束 、作废
        /// </summary>
        public ProcessStatusType CurrentStatus { get; private set; }

        /// <summary>
        /// 当前节点审批人名称  存储格式 admin【张三】,lisi【李四】
        /// </summary>
        public string? NodeApprovalName { get; private set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplyPersonName { get; private set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }

        /// <summary>
        /// 召回申请关联病历中间集合
        /// </summary>
        public virtual ICollection<RecallApplyAndArchival> RecallApplyAndArchivals { get; set; } = new HashSet<RecallApplyAndArchival>();

        /// <summary>
        /// 召回审批集合
        /// </summary>
        public virtual ICollection<RecallApprover> ArchiveApprovers { get; set; } = new HashSet<RecallApprover>();

        public RecallApply SetRecallApplyAndArchivals(ICollection<RecallApplyAndArchival> recallApplyAndArchivals)
        {
            RecallApplyAndArchivals = recallApplyAndArchivals;
            return this;
        }

        public RecallApply SetArchiveApprovers(ICollection<RecallApprover> archiveApprovers)
        {
            ArchiveApprovers = archiveApprovers;
            return this;
        }



        public RecallApply SetCurrentStatus(ProcessStatusType currentStatus)
        {
            CurrentStatus = currentStatus;
            return this;
        }

        public RecallApply SetCurrentApprovalNodeName(string? currentApprovalNodeName)
        {
            CurrentApprovalNodeName = currentApprovalNodeName;
            return this;
        }

        public RecallApply SetCurrentApprovalNodeId(string? currentApprovalNodeId)
        {
            CurrentApprovalNodeId = currentApprovalNodeId;
            return this;
        }

        public RecallApply SetIsEnd(bool isEnd)
        {
            IsEnd = isEnd;
            return this;
        }
        public RecallApply SetNodeApprovalName(string? nodeApprovalName)
        {
            NodeApprovalName = nodeApprovalName;
            return this;
        }
    }
}