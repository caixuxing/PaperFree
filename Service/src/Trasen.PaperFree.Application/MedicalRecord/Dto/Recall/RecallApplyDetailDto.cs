using Trasen.PaperFree.Application.Dto;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.Recall
{
    /// <summary>
    /// 召回审批详细Dto
    /// </summary>
    public class RecallApplyDetailDto
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 上级节点
        /// </summary>
        public string UpperNodeId { get; set; }

        /// <summary>
        /// 是否可驳回指定节点
        /// </summary>
        public bool? IsRejectToNode { get; set; }

        /// <summary>
        /// 流程节点集合
        /// </summary>
        public List<DropSelectDto<string>> NodeList { get; set; } = new();

        /// <summary>
        /// 事件方向分支
        /// </summary>
        public List<DropSelectDto<int>> eventDirectionTypes { get; set; } = new();
    }
}