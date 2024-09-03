namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.EssentialDocument
{
    public record EssentialDocumentsPageListDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }
        /// <summary>
        /// 目录编码
        /// </summary>
        public string FatherMeumid { get; set; }
        /// <summary>
        /// 目录类型（1、目录，2、节点）（当目录里子节点全选时只传父目录信息）
        /// </summary>
        public string MeumType { get; set; }
        /// <summary>
        /// 状态 状态（0、正常，1、作废）
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
    }
}