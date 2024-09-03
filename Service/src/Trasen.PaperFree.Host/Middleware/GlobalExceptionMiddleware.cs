using Hangfire;
using IPTools.Core;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.Shared.Response;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Host.Middleware
{
    /// <summary>
    /// 全局异常处理中间件
    /// 调用 app.UseMiddlewareGlobalExceptionMiddleware>();
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<GlobalExceptionMiddleware> logger;
        private readonly ICurrentUser currentUser;
        private readonly IEmail email;

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        /// <param name="appLifetime"></param>
        /// <param name="scopeFactory"></param>
        /// <param name="logger"></param>
        public GlobalExceptionMiddleware(RequestDelegate next,
            IHostApplicationLifetime appLifetime,
            IServiceScopeFactory scopeFactory,
            ILogger<GlobalExceptionMiddleware> logger,
            ICurrentUser currentUser,
            IEmail email)
        {
            this.next = next;
            this.appLifetime = appLifetime;
            this.scopeFactory = scopeFactory;
            this.logger = logger;
            this.currentUser = currentUser;
            this.email = email;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            LogLevel logLevel = LogLevel.Information;
            ResultCode code = ResultCode.SUCCESS;
            string msg;
            string error = string.Empty;
            object? data = null;
            bool notice = false;
            bool isMulti=false;
            MessageType type = MessageType.None;
            if (ex is Domain.Shared.CustomException.BusinessException customException)
            {
                msg = customException.ShowMessage ?? string.Empty;
                error = customException.Message;
                code = customException.ResultCode;
                data = customException.Data;
                type = customException.MessageType;
                isMulti=customException.IsMulti;
            }
            else
            {
                msg = "服务器好像出了点问题，请联系系统管理员...";
                error = $"{ex.Message}：InnerException{ex.InnerException?.Message}";
                logLevel = LogLevel.Error;
                code = ResultCode.GLOBAL_ERROR;
                type = MessageType.Error;
            }
            ApiResult<object> apiResult = new ApiResult<object>(type, code, msg, data,isMulti);
            string responseResult = JsonConvert.SerializeObject(apiResult, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
            string ip = HttpContextExtension.GetClientUserIp(context);
            var ip_info = IpTool.Search(ip);
            var endpoint = GetEndpoint(context);
            var logAttribute = endpoint?.Metadata.GetMetadata<LogAttribute>();

            SysOperLog sysOperLog = new(logAttribute?.Title, logAttribute?.BusinessType, context.Request.Path, context.Request.Method,
                BperatorType.BACKGROUND, currentUser.UserName,
             HttpContextExtension.GetRequestUrl(context), ip,
            $"{ip_info.Province.Replace("0", "")}{ip_info.City};{ip_info.NetworkOperator}",
             HttpContextExtension.GetRequestValue(context, context.Request.Method),
            responseResult,
            StatusLogType.EXCEPTION, error, DateTime.Now, 0, currentUser.OrgCode, currentUser.HospCode, currentUser.InputCode);
            context.Response.ContentType = "text/json;charset=utf-8";
            await context.Response.WriteAsync(responseResult, System.Text.Encoding.UTF8);
            string errorMsg = $"> 操作人：{sysOperLog.OperName}" +
                $"\n> 操作地区：{sysOperLog.OperIp}({sysOperLog.OperAddr})" +
                $"\n> 操作模块：{sysOperLog.Title}" +
                $"\n> 操作地址：{sysOperLog.RequestUrl}" +
                $"\n> 错误信息：{msg}\n\n> {error}";
            logger.LogError(errorMsg);
            using (var scope = scopeFactory.CreateScope())
            {
                ISysOperLogRepo sysOperLogRepo = scope.ServiceProvider.GetRequiredService<ISysOperLogRepo>();
                await sysOperLogRepo.AddAsync(sysOperLog, CancellationToken.None);
                IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                await unitOfWork.SaveChangesAsync();
            }
            //推送系统异常信息
            if (!notice)
            {
                var body = new MimeKit.BodyBuilder();
                body.TextBody = errorMsg;
                var message = new MimeKit.MimeMessage();
                message.Subject = "系统异常通知";
                message.Body = body.ToMessageBody();
                BackgroundJob.Enqueue("log-queue", () => email.SendEmailAsync(message));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Endpoint? GetEndpoint(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return context.Features.Get<IEndpointFeature>()?.Endpoint;
        }
    }
}