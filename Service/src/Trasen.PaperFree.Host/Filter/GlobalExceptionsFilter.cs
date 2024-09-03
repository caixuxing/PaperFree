using Microsoft.AspNetCore.Mvc.Filters;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Filter
{
    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionsFilter> _loggerHelper;

        /// <summary>
        ///构造方法
        /// </summary>
        /// <param name="loggerHelper"></param>
        public GlobalExceptionsFilter(ILogger<GlobalExceptionsFilter> loggerHelper)
        {
            _loggerHelper = loggerHelper;
        }

        /// <summary>
        ///全局异常捕获
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            //判断异常是否已处理
            if (!context.ExceptionHandled)
            {
                //程序内部错误捕获
                context.ExceptionHandled = true;
                context.Result = new ObjectResult(new ApiResult<object>(MessageType.Error, ResultCode.GLOBAL_ERROR, "系统内部错误", default));

                //TODO：记录内部错误真实异常信息
            }
        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }
    }
}