using Trasen.PaperFree.Application.SystemBasicInfo.Query;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers;

/// <summary>
/// 授权
/// </summary>
[Route("api/auth")]
[ApiController]
public class AuthController : Controller
{
    /// <summary>
    ///
    /// </summary>
    private readonly IMediator mediator;

    /// <summary>
    ///
    /// </summary>
    /// <param name="mediator"></param>
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost, Route("login")]
    [Log(Title = "登录", BusinessType = BusinessType.LOGIN)]
    public async Task<IActionResult> Login([FromBody] LoginQry request)
    {
        var result = await mediator.Send(request);
        return ObjectResponse.Ok(result);
    }
}