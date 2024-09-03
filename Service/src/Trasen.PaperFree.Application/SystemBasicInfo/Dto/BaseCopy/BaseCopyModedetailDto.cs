namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseCopy
{
    public class BaseCopyModedetailDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 目录id
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 状态（有效，无效） 8
        /// </summary>
        public string Status { get; set; }
    }
}