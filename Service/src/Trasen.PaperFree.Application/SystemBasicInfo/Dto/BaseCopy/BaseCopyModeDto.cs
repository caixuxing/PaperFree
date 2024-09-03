namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseCopy
{
    public class BaseCopyModeDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string ModeName { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 辖区编码
        /// </summary>
        public string InputCode { get; set; }

        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double Pay { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}