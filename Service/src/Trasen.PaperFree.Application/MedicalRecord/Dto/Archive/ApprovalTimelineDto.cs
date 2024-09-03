namespace Trasen.PaperFree.Application.MedicalRecord.Dto.Archive
{
    /// <summary>
    /// 归档审批时间轴Dto
    /// </summary>
    public record ApprovalTimelineDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string AdmissId { get; set; }

        /// <summary>
        /// 审批状态流转信息
        /// </summary>
        public List<ApprovalStatusFlowValueObj> approvalStatusFlows { get; set; } = new List<ApprovalStatusFlowValueObj>();
    }

    /// <summary>
    /// 审批状态流转信息
    /// </summary>
    public record ApprovalStatusFlowValueObj
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}