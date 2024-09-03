namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseBorrowMode
{
    public class BaseBorrowModeDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// 模板名称
        /// </summary>
        public string ModeName { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public string IsEnable { get; set; }
    }
}