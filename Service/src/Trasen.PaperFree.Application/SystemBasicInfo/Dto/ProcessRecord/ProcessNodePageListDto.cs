using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord
{
    public record ProcessNodePageListDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 流程主键ID
        /// </summary>
        public string ProcessDesignId { get; set; }
        /// <summary>
        /// 流程设计名称
        /// </summary>
        public string ProcessDesignName { get; set; }

        public ProcessTempType ProcessTempType { get; set; }
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

        public string UpperNodeName { get; set; }

        /// <summary>
        /// 下级节点 下游节点（最后一步结束）否则下级审批节点
        /// </summary>
        public string LowerNodeId { get; set; }
        /// <summary>
        /// 下级节点名称
        /// </summary>
        public string LowerNodeName { get; set; }

        /// <summary>
        /// 节点流程状态
        /// </summary>
        public int NodeMapWorkflowStatus { get; set; }
        /// <summary>
        /// 节点流程状态名称
        /// </summary>
        public string NodeMapWorkflowStatusName { get; set; }

        /// <summary>
        /// 当前节点审批人员
        /// </summary>
        public string CurrentNodeApprovers { get; set; }

        /// <summary>
        /// 排序号 顺序，值越小越靠前，否则反之
        /// </summary>
        public int OderNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}