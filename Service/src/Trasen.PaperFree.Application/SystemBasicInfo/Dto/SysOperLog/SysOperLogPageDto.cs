using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.SysOperLog
{
    /// <summary>
    /// 系统操作日志分页Dto
    /// </summary>
    public record SysOperLogPageDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessTypeName { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestType { get; set; }
        /// <summary>
        /// 操作类别
        /// </summary>
        public string OperatorTypeName { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string? OperName { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime { get; set; }
        /// <summary>
        /// 操作用时
        /// </summary>
        public long Elapsed { get; set; }
    }
}