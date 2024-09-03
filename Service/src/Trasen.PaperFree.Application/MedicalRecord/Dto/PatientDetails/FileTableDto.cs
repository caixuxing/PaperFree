namespace Trasen.PaperFree.Application.MedicalRecord.Dto.PatientDetails
{
    /// <summary>
    /// 文件信息Dto
    /// </summary>
    public record FileTableDto
    {
        public string ArchiveId { get; set; }
        /// <summary>
        /// 病历目录表ID
        /// </summary>
        public string MeumId { get; set; }
        /// <summary>
        /// 文件唯-ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 文件保存后名称
        /// </summary>
        public string FileSavename { get; set; }

        /// <summary>
        /// 文件存储路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string OrderNo { get; set; }
    }
}