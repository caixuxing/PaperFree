using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query;
using Trasen.PaperFree.Infrastructure.SeedWork.Redis;
using Trasen.PaperFree.Infrastructure.SignalR;

namespace Trasen.PaperFree.Host.Controllers;

/// <summary>
/// 系统基础信息
/// </summary>
[Route("api/systembasicinfo")]
[ApiController]
[Authorize()]
public class SystemBasicInfoController : ControllerBase
{
    /// <summary>
    ///
    /// </summary>
    private readonly IMediator _mediator;

    private readonly ILogger<AuthController> logger;
    private readonly IHubContext<PersonHub> _hubContext;
    private readonly IRedisService _redisService;
    private readonly ICurrentUser _user;
    /// <summary>
    /// 注入构建
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    /// <param name="hubContext"></param>
    /// <param name="redisService"></param>
    /// <param name="user"></param>
    public SystemBasicInfoController(IMediator mediator, ILogger<AuthController> logger, IHubContext<PersonHub> hubContext, IRedisService redisService, ICurrentUser user)
    {
        _mediator = mediator;
        this.logger = logger;
        _hubContext = hubContext;
        _redisService = redisService;
        _user = user;
    }

    /// <summary>
    /// 获取用户菜单
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("getusermenu")]
    [ProducesResponseType(typeof(List<FindSystemMenuByCurrentUserDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUserMenuInfo(CancellationToken cancellationToken)
    {
        //发起一个后台作业任务
        var jobId = BackgroundJob.Enqueue("img-queue", () => PostAsync(_user.Id));

        return Ok(await _mediator.Send(new FindSystemMenuByCurrentUserQuery(), cancellationToken));
    }

    /// <summary>
    /// 获取机构
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("org")]
    [ProducesResponseType(typeof(List<FindOrgDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrg(CancellationToken cancellationToken) => Ok(await _mediator.Send(new FindOrgQuery(), cancellationToken));

    /// <summary>
    /// 获取科室
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("orgdepartment")]
    [ProducesResponseType(typeof(List<FindOrgDepartmentDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrgDepartment(CancellationToken cancellationToken) => Ok(await _mediator.Send(new FindOrgDepartmentQuery(), cancellationToken));

    /// <summary>
    /// 获取系统参数
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("sysparameterlist")]
    [ProducesResponseType(typeof(List<FindSysParameterListByPageDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetSysParameterListByPage(CancellationToken cancellationToken) => Ok(await _mediator.Send(new FindSysParameterListByPageQuery(), cancellationToken));

    /// <summary>
    /// 获取个人参数
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("personalparameterlist")]
    [ProducesResponseType(typeof(List<FindPersonalParameterListByPageDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPersonalParameterListByPage(CancellationToken cancellationToken) => Ok(await _mediator.Send(new FindPersonalParameterListByPageQuery(), cancellationToken));

    /// <summary>
    /// 获取机构参数
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("orgparameterlist")]
    [ProducesResponseType(typeof(List<FindOrgParameterListByPageDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrgParameterListByPage(CancellationToken cancellationToken) => Ok(await _mediator.Send(new FindOrgParameterListByPageQuery(), cancellationToken));

    /// <summary>
    /// 获取单个人员与科室关系表
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("orgdeptmemberpagelist")]
    [ProducesResponseType(typeof(List<FindOrgDeptMemberPageListDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrgDeptMemberPageList(CancellationToken cancellationToken) => Ok(await _mediator.Send(new FindOrgDeptMemberPageListQuery(), cancellationToken));

    /// <summary>
    /// 定义一个后台作业【任务】
    ///
    /// 假如我们登录后> 有件非常耗时的操作（3分钟才能完成）
    /// 如果同步执行 影响登录功能体验
    ///
    /// </summary>
    /// <returns></returns>

    [Queue("img-queue")]
    [DisableConcurrentExecution(timeoutInSeconds: 180)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task PostAsync(string id)
    {
        var t = await Task.FromResult(0);
        await Task.Delay(9000);
        logger.LogInformation($"执行zhong....{id}");
        var KeyId = await _redisService.HGetAsync(RedisKeys.SignalRConnection, id);
        if (KeyId is null) return;
        await _hubContext.Clients.Client(KeyId).SendAsync("SendMessage", $"当前登录用户:{id},登录任务已执行完毕！");
    }
}