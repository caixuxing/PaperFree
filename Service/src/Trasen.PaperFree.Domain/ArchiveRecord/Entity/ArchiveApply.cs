using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.CustomException;
using Trasen.PaperFree.Domain.Shared.Enums;

namespace Trasen.PaperFree.Domain.ArchiveRecord.Entity
{
    /// <summary>
    /// 归档申请表
    /// </summary>
    public record ArchiveApply : FullRoot
    {
        private ArchiveApply() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="archiveId">档案号ID</param>
        /// <param name="archiveName">归档名称</param>
        /// <param name="currentStatus"> 状态 流程状态：开始》审批中》结束 、作废</param>
        /// <param name="isEnd">是否结束</param>
        /// <param name="processDesignId">流程模板ID</param>
        /// <param name="currentApprovalNodeId">当前审批节点ID</param>
        /// <param name="orgCode">机构编码</param>
        /// <param name="hospCode">辖区编码</param>
        /// <param name="currentApprovalNodeName">当前审批节点名称</param>
        /// <param name="nodeApprovaName">当前节点审批人名称  存储格式 admin【张三】,lisi【李四】</param>
        /// <param name="applyPersonName">申请人姓名</param>
        public ArchiveApply(string archiveId, string archiveName,
            ProcessStatusType currentStatus, bool isEnd, string processDesignId,
            string currentApprovalNodeId, string orgCode, string hospCode,
            string? currentApprovalNodeName, string? nodeApprovaName, string applyPersonName)
        {
            ArchiveId = archiveId;
            ArchiveName = archiveName;
            CurrentStatus = currentStatus;
            IsEnd = isEnd;
            ProcessDesignId = processDesignId;
            CurrentApprovalNodeId = currentApprovalNodeId;
            OrgCode = orgCode;
            HospCode = hospCode;
            if (string.IsNullOrWhiteSpace(this.HospCode))
            {
                throw new BusinessException(Shared.Response.MessageType.Warn, "院区编码不能为空！");
            }
            CurrentApprovalNodeName = currentApprovalNodeName;
            NodeApprovaName = nodeApprovaName;
            if (NodeApprovaName?.Length>1000)
            {
                throw new BusinessException(Shared.Response.MessageType.Warn, "当前审批人员字符串长度超出！");
            }
            ApplyPersonName = applyPersonName;
        }

        /// <summary>
        /// 设置当前节点审批人员
        /// </summary>
        /// <param name="nodeApprovaName"></param>
        /// <returns></returns>
        public ArchiveApply SetNodeApprovaName(string? nodeApprovaName)
        {
            NodeApprovaName = nodeApprovaName;
            return this;
        }

        public ArchiveApply SetIsEnd(bool isEnd)
        {
            IsEnd = isEnd;
            return this;
        }

        /// <summary>
        /// 变更归档申请状态
        /// </summary>
        /// <param name="CurrentStatus"></param>
        /// <returns></returns>
        public ArchiveApply SetCurrentStatus(ProcessStatusType CurrentStatus)
        {
            this.CurrentStatus = CurrentStatus;
            return this;
        }

        /// <summary>
        /// 设置当前审批节点ID
        /// </summary>
        /// <param name="currentApprovalNodeId"></param>
        /// <returns></returns>
        public ArchiveApply SetCurrentApprovalNodeId(string? currentApprovalNodeId)
        {
            this.CurrentApprovalNodeId = currentApprovalNodeId;
            return this;
        }

        public string ArchiveId { get; private set; }
        /// <summary>
        /// 申请名称 默认为当前病案人归档作为名称，批量为批量归档申请
        /// </summary>
        public string ArchiveName { get; private set; }

        /// <summary>
        /// 状态 流程状态：开始》审批中》结束 、作废
        /// </summary>
        public ProcessStatusType CurrentStatus { get; private set; }
        /// <summary>
        /// 流程是否结束 状态：是 、否
        /// </summary>
        public bool IsEnd { get; private set; }
        /// <summary>
        /// 流程模板ID 绑定当前当前申请审批模板
        /// </summary>
        public string ProcessDesignId { get; private set; }
        /// <summary>
        /// 当前审批节点ID
        /// </summary>
        public string? CurrentApprovalNodeId { get; private set; }
        /// <summary>
        /// 当前审批节点名称
        /// </summary>
        public string? CurrentApprovalNodeName { get; private set; }

        /// <summary>
        /// 当前节点审批人名称  存储格式 admin【张三】,lisi【李四】
        /// </summary>
        public string? NodeApprovaName { get; private set; }
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
        /// 归档审批集合
        /// </summary>
        public virtual ICollection<ArchiveApprover> ArchiveApprovers { get; set; } = new HashSet<ArchiveApprover>();
    }
}