using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.ProcessRecord.Entity
{
    /// <summary>
    /// 流程节点表
    /// </summary>
    public record ProcessNode : FullRoot
    {
        public ProcessNode(string id, string processDesignId, string nodeName, string nodeCode,
            string upperNodeId, string lowerNodeId, string eventDirectionBranch, bool? isRejectToNode,  int oderNo,
            ICollection<NodeApprover> nodeApprovers, int nodeMapWorkflowStatus)
        {
            this.Id = id;
            ProcessDesignId = processDesignId;
            NodeName = nodeName;
            NodeCode = nodeCode;
            UpperNodeId = upperNodeId;
            LowerNodeId = lowerNodeId;
            EventDirectionBranch = eventDirectionBranch;
            IsRejectToNode = isRejectToNode;
            OderNo = oderNo;
            this.NodeApprovers = nodeApprovers;
            NodeMapWorkflowStatus = nodeMapWorkflowStatus;
        }

        private ProcessNode() { }

        /// <summary>
        /// 流程主键ID
        /// </summary>
        public string ProcessDesignId { get; private set; }

        public virtual ProcessDesign ProcessDesign { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; private set; }
        /// <summary>
        /// 节点代码值
        /// </summary>
        public string NodeCode { get; private set; }
        /// <summary>
        /// 上级节点 上游节点（第一步默认为开始）否则上级审批节点
        /// </summary>
        public string UpperNodeId { get; private set; }
        /// <summary>
        /// 下级节点 下游节点（最后一步结束）否则下级审批节点
        /// </summary>
        public string LowerNodeId { get; private set; }

        /// <summary>
        /// 节点对应流程状态
        /// </summary>
        public int NodeMapWorkflowStatus { get; private set; }

        /// <summary>
        /// 事件方向分支【通过、拒绝、驳回】
        /// </summary>
        public string EventDirectionBranch { get; set; }
        /// <summary>
        /// 是否可驳回指定节点 是否开启当前节点审批人员拥有回退到指定节点功能
        /// </summary>
        public bool? IsRejectToNode { get; set; }
        /// <summary>
        /// 排序号 顺序，值越小越靠前，否则反之
        /// </summary>
        public int OderNo { get; private set; }
        /// <summary>
        /// 节点审批人员
        /// </summary>
        public virtual ICollection<NodeApprover> NodeApprovers { get; set; } = new HashSet<NodeApprover>();

        public ProcessNode SetProcessDesignId(string processDesignId)
        {
            ProcessDesignId = processDesignId;
            return this;
        }
        public ProcessNode SetNodeName(string nodeName)
        {
            NodeName = nodeName;
            return this;
        }

        public ProcessNode SetNodeApprovers(ICollection<NodeApprover> nodeApprovers)
        {
            NodeApprovers = nodeApprovers;
            return this;
        }

        public ProcessNode SetOderNo(int oderNo)
        {
            OderNo = oderNo;
            return this;
        }
        public ProcessNode SetIsRejectToNode(bool? isRejectToNode)
        {
            IsRejectToNode = isRejectToNode;
            return this;
        }

        public ProcessNode SetEventDirectionBranch(string eventDirectionBranch)
        {
            EventDirectionBranch = eventDirectionBranch;
            return this;
        }

        public ProcessNode SetNodeMapWorkflowStatus(int nodeMapWorkflowStatus)
        {
            NodeMapWorkflowStatus = nodeMapWorkflowStatus;
            return this;
        }

        public ProcessNode SetLowerNodeId(string lowerNodeId)
        {
            LowerNodeId = lowerNodeId;
            return this;
        }

        public ProcessNode SetUpperNodeId(string upperNodeId)
        {
            UpperNodeId = upperNodeId;
            return this;
        }

        public ProcessNode SetNodeCode(string nodeCode)
        {
            NodeCode = nodeCode;
            return this;
        }
    }
}