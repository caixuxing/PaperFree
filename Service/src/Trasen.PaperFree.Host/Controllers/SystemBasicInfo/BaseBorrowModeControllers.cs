using Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseBorrowMode;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.BaseBorrowMode;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 借阅模板设置
    /// </summary>
    [Route("api/ProcessDesign")]
    [ApiController]
    public class BaseBorrowModeControllers : ControllerBase
    {
        private readonly IMediator mediator;
/// <summary>
/// 注入
/// </summary>
/// <param name="mediator"></param>
        public BaseBorrowModeControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #region// 借阅模板设置

        /// <summary>
        ///创建借阅模板
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateBaseBorrowMode")]
        [Log(Title = "创建借阅模板", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateBaseBorrowMode([FromBody] CreateBaseBorrowModeCmd request)
            => ObjectResponse.Ok("创建成功", await mediator.Send(request));

        /// <summary>
        /// 更新流程节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("BaseBorrowMode/{id}")]
        [Log(Title = "更新借阅模板", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateBaseBorrowMode([FromRoute] string id, [FromBody] ModifyBaseBorrowModeCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        /// <summary>
        ///删除借阅模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("BaseBorrowMode/{id}")]
        [Log(Title = "删除借阅模板", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteBaseBorrowMode([FromRoute] string id)
            => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteBaseBorrowModeCmd(id)));

        /// <summary>
        ///按ID查找借阅模板信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("BaseBorrowModeById/{id}")]
        [Log(Title = "按ID查找借阅模板信息", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindBaseBorrowModeById([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindBaseBorrowModeByIdQry(id)));

        #endregion
    }
}