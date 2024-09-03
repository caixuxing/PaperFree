using IPTools.Core;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Diagnostics;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.Shared.Response;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using ResultCode = Trasen.PaperFree.Domain.Shared.Response.ResultCode;

namespace Trasen.PaperFree.Host.Filter
{
    /// <summary>
    /// 全局Action监控
    /// </summary>
    public class GlobalActionMonitor : ActionFilterAttribute
    {
        private readonly ILogger<GlobalActionMonitor> _logger;
        private readonly ISysOperLogRepo _sysOperLogRepo;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public GlobalActionMonitor(ILogger<GlobalActionMonitor> logger, ISysOperLogRepo sysOperLogRepo,
            IGuidGenerator guidGenerator, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _logger = logger;
            _sysOperLogRepo = sysOperLogRepo;
            _guidGenerator = guidGenerator;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        /// <summary>
        /// 请求时长计时开始
        /// </summary>
        private readonly Stopwatch watch = new Stopwatch();

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            watch.Restart();
            await base.OnActionExecutionAsync(context, next);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public override async void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.ActionDescriptor is not ControllerActionDescriptor controllerActionDescriptor) return;
            //获得注解信息
            LogAttribute? logAttribute = GetLogAttribute(controllerActionDescriptor);
            try
            {
                string method = context.HttpContext.Request.Method.ToUpper();
                string reuqtpram = HttpContextExtension.GetRequestValue(context.HttpContext, method);
                // 获取当前的用户
                string userName = currentUser.UserName;
                string jsonResult = string.Empty;
                ApiResult<object> apiResult = new(MessageType.Info, ResultCode.SUCCESS, "", ((OkObjectResult)context.Result).Value,false);
                jsonResult = JsonConvert.SerializeObject(apiResult);
                string controller = context.RouteData.Values["Controller"].ToString();
                string action = context.RouteData.Values["Action"].ToString();
                string ip = HttpContextExtension.GetClientUserIp(context.HttpContext);
                var ip_info = IpTool.Search(ip);
                watch.Stop();
                SysOperLog sysOperLog = new(logAttribute?.Title, logAttribute?.BusinessType, $"{controller}.{action}()", method, BperatorType.BACKGROUND, userName ?? string.Empty,
                    HttpContextExtension.GetRequestUrl(context.HttpContext), ip,
                    $"{ip_info.Province.Replace("0", "")}{ip_info.City};{ip_info.NetworkOperator}",
                    HttpContextExtension.GetRequestValue(context.HttpContext, method),
                    (logAttribute?.IsSaveResponseData ?? false) ? jsonResult ?? string.Empty : string.Empty,
                    StatusLogType.NORMAL, string.Empty, DateTime.Now, watch.ElapsedMilliseconds, string.Empty, string.Empty, string.Empty);
                // BackgroundJob.Enqueue("log-queue", () => GlobalActionMonitorLogAsync(sysOperLog));

                await GlobalActionMonitorLogAsync(sysOperLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"记录操作日志出错了#{ex.Message}");
            }
        }

        /// <summary>
        /// 定义一个后台作业【任务】
        ///
        /// 假如我们登录后> 有件非常耗时的操作（3分钟才能完成）
        /// 如果同步执行 影响登录功能体验
        ///
        /// </summary>
        /// <returns></returns>

        public async Task GlobalActionMonitorLogAsync(SysOperLog sysOperLog)
        {
            await _sysOperLogRepo.AddAsync(sysOperLog, CancellationToken.None);
            await unitOfWork.SaveChangesAsync();
        }

        private LogAttribute? GetLogAttribute(ControllerActionDescriptor controllerActionDescriptor)
        {
            var attribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                .FirstOrDefault(a => a.GetType().Equals(typeof(LogAttribute)));
            return attribute as LogAttribute;
        }
    }
}