using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.SysOperLog
{
    public class SysOperLogDetailDto
    {
        /// <summary>
        /// 操作模块
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessType? BusinessType { get; set; }

        public string? BusinessTypeName { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// 操作类别
        /// </summary>
        public BperatorType OperatorType { get; set; }

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
        /// 操作Ip地址
        /// </summary>
        public string OperIp { get; set; }

        /// <summary>
        /// 操作地址
        /// </summary>
        public string? OperAddr { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string? RequestParam { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public string? JsonResult { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public StatusLogType Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMsg { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime { get; set; }

        /// <summary>
        /// 操作用时
        /// </summary>
        public long Elapsed { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string? OrgCode { get; set; }

        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; set; }

        /// <summary>
        /// 辖区编码
        /// </summary>
        public string? InputCode { get; set; }
    }
}