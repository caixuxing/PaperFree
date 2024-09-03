using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 系统操作日志
    /// </summary>
    public record SysOperLog : FullRoot
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="title">操作模块</param>
        /// <param name="businessType">业务类型</param>
        /// <param name="requestMethod">请求方法</param>
        /// <param name="requestType">请求类型</param>
        /// <param name="operatorType">操作类别</param>
        /// <param name="operName">操作人</param>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="operIp">Ip</param>
        /// <param name="operAddr">Ip地址</param>
        /// <param name="requestParam">请求参数</param>
        /// <param name="jsonResult">返回结果</param>
        /// <param name="status">状态</param>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="operTime">操作时间</param>
        /// <param name="elapsed">耗时</param>
        /// <param name="orgCode">机构</param>
        /// <param name="hospCode">院区</param>
        /// <param name="inputCode">辖区</param>
        public SysOperLog(string? title, BusinessType? businessType, string requestMethod, string requestType, BperatorType operatorType,
            string? operName, string requestUrl, string operIp, string? operAddr, string? requestParam, string? jsonResult, StatusLogType status,
            string? errorMsg, DateTime operTime, long elapsed, string? orgCode, string? hospCode, string? inputCode)
        {
            Title = title;
            BusinessType = businessType;
            RequestMethod = requestMethod;
            RequestType = requestType;
            OperatorType = operatorType;
            OperName = operName;
            RequestUrl = requestUrl;
            OperIp = operIp;
            OperAddr = operAddr;
            RequestParam = requestParam;
            JsonResult = jsonResult;
            Status = status;
            ErrorMsg = errorMsg;
            OperTime = operTime;
            Elapsed = elapsed;
            OrgCode = orgCode;
            HospCode = hospCode;
            InputCode = inputCode;
        }

        /// <summary>
        /// 操作模块
        /// </summary>
        public string? Title { get; private set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessType? BusinessType { get; private set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        public string RequestMethod { get; private set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestType { get; private set; }
        /// <summary>
        /// 操作类别
        /// </summary>
        public BperatorType OperatorType { get; private set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string? OperName { get; private set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string RequestUrl { get; private set; }
        /// <summary>
        /// 操作Ip地址
        /// </summary>
        public string OperIp { get; private set; }
        /// <summary>
        /// 操作地址
        /// </summary>
        public string? OperAddr { get; private set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string? RequestParam { get; private set; }
        /// <summary>
        /// 返回参数
        /// </summary>
        public string? JsonResult { get; private set; }
        /// <summary>
        /// 操作状态
        /// </summary>
        public StatusLogType Status { get; private set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMsg { get; private set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime { get; private set; }
        /// <summary>
        /// 操作用时
        /// </summary>
        public long Elapsed { get; private set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string? OrgCode { get; private set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; private set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string? InputCode { get; private set; }
    }
}