using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.ProcessRecord.Entity
{
    /// <summary>
    /// 节点审批人员
    /// </summary>
    public record NodeApprover : FullRoot
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="processNodeId"></param>
        /// <param name="approverId"></param>
        /// <param name="approverAccount"></param>
        /// <param name="approverName"></param>
        public NodeApprover(string id, string processNodeId, string approverId,
            string approverAccount, string approverName)
        {
            this.Id = id;
            ProcessNodeId = processNodeId;
            ApproverId = approverId;
            ApproverAccount = approverAccount;
            ApproverName = approverName;
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string ProcessNodeId { get; private set; }

        public virtual ProcessNode ProcessNode { get; private set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public string ApproverId { get; private set; }

        /// <summary>
        /// 审批人账户
        /// </summary>
        public string ApproverAccount { get; private set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string ApproverName { get; set; }
    }
}