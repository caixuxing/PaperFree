namespace Trasen.PaperFree.Application.MedicalRecord.Dto.Archive
{
    /// <summary>
    /// 归档申请分页列表Dto
    /// </summary>
    public record ArchiveApplyPageDto
    {
        /// <summary>
        /// 归档申请主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 归档申请名称
        /// </summary>
        public string ArchiveName { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        public string AdmissId { get; set; }

        /// <summary>
        /// 当前审批节点名称
        /// </summary>
        public string CurrentApprovalNodeName { get; set; }
        /// <summary>
        /// 当前节点审批人
        /// </summary>
        public string CurrentNodeApprovalPerson { get; set; }
        /// <summary>
        /// 下一审批节点名称
        /// </summary>
        public string NextApprovalNodeName { get; set; }
        /// <summary>
        /// 当前审批状态名称
        /// </summary>
        public string CurrentStatusName { get; set; }
        /// <summary>
        /// 当前审批状态
        /// </summary>
        public ProcessStatusType CurrentStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 归档申请人名称
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// 流程是否结束
        /// </summary>
        public bool IsEnd { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 院区名称
        /// </summary>
        public string HospName { get; set; }
    }
}