using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 复印模板明细表
    /// </summary>
    public record BaseCopyModedetail : FullRoot
    {
        private BaseCopyModedetail() { }
        /// <summary>
        /// 模板id
        /// </summary>
        public string ModeId { get; private set; }
        /// <summary>
        /// 目录id
        /// </summary>
        public string MenuId { get; private set; }
        /// <summary>
        /// 状态（有效，无效） 8
        /// </summary>
        public string Status { get; private set; }
    }
}