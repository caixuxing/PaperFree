using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 复印模板设置
    /// </summary>
    public record BaseCopyMode : FullRoot
    {
        public BaseCopyMode() { }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string ModeName { get; private set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; private set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string InputCode { get; private set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; private set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Pay { get; private set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; private set; }
    }
}