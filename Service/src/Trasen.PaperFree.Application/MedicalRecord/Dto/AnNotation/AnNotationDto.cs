namespace Trasen.PaperFree.Application.MedicalRecord.Dto.AnNotation
{
    public record AnNotationDto
    {
        /// <summary>
        /// 档案号 档案号
        /// </summary>
        public string Archiveid { get; set; }
        /// <summary>
        /// 文件id 文件id
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        //批注内容 批注内容
        public string AnNotAtIOn { get; set; }

        //批注科室编码 批注科室编码
        public string Deptcode { get; set; }

        //备注 备注
        public string Remark { get; set; }

        /// <summary>
        /// 评分 评分
        /// </summary>
        public string Lower { get; set; }
        /// <summary>
        /// 批注日期 批注日期
        /// </summary>
        public DateTime? AnNotAtIOnDate { get; set; }

        public string Id { get; set; }
    }
}