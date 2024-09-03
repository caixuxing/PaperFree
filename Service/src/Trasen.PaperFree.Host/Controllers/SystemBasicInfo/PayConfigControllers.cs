using Trasen.PaperFree.Application.SystemBasicInfo.Commands.PayConfig;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.PayConfigQuery;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 支付配置表
    /// </summary>
    [Route("api/PayConfig")]
    [ApiController]
    public class PayConfigControllers : ControllerBase
    {
        private readonly IMediator mediator;

        public PayConfigControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// 新增支付配置表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreatePayConfig")]
        [Log(Title = "新增支付配置表", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateBaseWatermar([FromBody] CreatePayConfigCmd request)
             => ObjectResponse.Ok("创建成功", await mediator.Send(request));

        /// <summary>
        /// 修改支付配置表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request">修改内容</param>
        /// <returns></returns>
        [HttpPut, Route("UpdatePayConfig/{id}")]
        [Log(Title = "修改支付配置表", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateBaseWatermark([FromRoute] string id, [FromBody] ModifyPayConfigCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        /// <summary>
        /// 删除支付配置表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeletePayConfig/{id}")]
        [Log(Title = "删除支付配置表", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteBaseWatermark([FromRoute] string id)
         => ObjectResponse.Ok("删除成功", await mediator.Send(new DeletePayConfigCmd(id)));

        [HttpGet, Route("PayConfigById/{id}")]
        [Log(Title = "按ID查找支付配置表数据", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindBaseWatermarkById([FromRoute] string id)
        => ObjectResponse.Ok("ok", await mediator.Send(new FindPayConfigByIdQry(id)));

        /// <summary>
        /// 支付管理列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="qry"></param>
        /// <returns></returns>
        [HttpPost, Route("PayConfigPageList/{pageindex}/{pagesize}")]
        [Log(Title = "支付管理列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> PayConfigPageList([FromRoute] int pageindex, [FromRoute] int pagesize, [FromBody] FindPayConfigPageListQry qry)
        {
            qry.SetPageParm(pageindex, pagesize);
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }
    }
}