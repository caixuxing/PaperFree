namespace Trasen.PaperFree.Domain.Shared.Response
{
    /// <summary>
    /// 数据返回模型基类
    /// </summary>
    public class ApiResult<T>
    {
        public ApiResult(MessageType messageType, ResultCode resultCode, string? message, T? data,bool isMulti)
        {
            ResultCode = resultCode;
            this.MessageType = messageType;
            Message = message;
            Data = data;
            IsMulti = isMulti;
        }

        public ApiResult()
        { }

        /// <summary>
        /// 返回状态码
        /// </summary>
        public virtual ResultCode ResultCode { get; set; } = ResultCode.SUCCESS;

        /// <summary>
        /// 信息类型
        /// </summary>
        public virtual MessageType MessageType { get; set; } = MessageType.None;

        /// <summary>
        /// 是否多错误
        /// </summary>
        public virtual bool IsMulti { get; set; }=false;

        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public virtual string? Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public virtual T? Data { get; set; }
    }
}