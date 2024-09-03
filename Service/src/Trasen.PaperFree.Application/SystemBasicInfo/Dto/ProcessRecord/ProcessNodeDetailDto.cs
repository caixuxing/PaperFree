using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord
{
    public class ProcessNodeDetailDto
    {
        public string ID { get; set; }

        /// <summary>
        /// 流程主键ID
        /// </summary>
        public string ProcessDesignId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 节点代码值
        /// </summary>
        public string NodeCode { get; set; }

        /// <summary>
        /// 上级节点 上游节点（第一步默认为开始）否则上级审批节点
        /// </summary>
        public string UpperNodeId { get; set; }

        /// <summary>
        /// 下级节点 下游节点（最后一步结束）否则下级审批节点
        /// </summary>
        public string LowerNodeId { get; set; }

        /// <summary>
        /// 事件方向
        /// </summary>
        public List<EventDirectionType> EventDirectionBranch { get; set; }

        /// <summary>
        /// 节点流程状态
        /// </summary>
        public int NodeMapWorkflowStatus { get; set; }

        /// <summary>
        /// 是否可驳回指定节点 是否开启当前节点审批人员拥有回退到指定节点功能
        /// </summary>
        public bool? IsRejectToNode { get; set; }

        /// <summary>
        /// 排序号 顺序，值越小越靠前，否则反之
        /// </summary>
        public int OderNo { get; set; }

        /// <summary>
        /// 节点审批人信息
        /// </summary>
        public virtual ICollection<NodeApproverValueObj> NodeApprovers { get; set; }
    }
}