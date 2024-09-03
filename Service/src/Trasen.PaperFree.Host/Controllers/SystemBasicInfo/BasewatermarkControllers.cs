using Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseWatermark;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.BaseWatermark;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 水印表操作
    /// </summary>
    [Route("api/Basewatermark")]
    [ApiController]
    public class BasewatermarkControllers : ControllerBase
    {
        private readonly IMediator mediator;
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="mediator"></param>
        public BasewatermarkControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// 新增水印表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateBasewatermark")]
        [Log(Title = "新增水印表数据", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateBaseWatermar([FromBody] CreateBaseWatermarkCmd request)
         => ObjectResponse.Ok("创建成功", await mediator.Send(request));

        /// <summary>
        /// 修改水印表数据
        /// </summary>
        /// <param name="id">水印ID</param>
        /// <param name="request">修改内容</param>
        /// <returns></returns>
        [HttpPut, Route("UpdateBasewatermark/{id}")]
        [Log(Title = "修改水印表数据", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateBaseWatermark([FromRoute] string id, [FromBody] ModifyBaseWatermarkCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        /// <summary>
        /// 删除水印表数据
        /// </summary>
        /// <param name="id">水印数据ID</param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteBasewatermark/{id}")]
        [Log(Title = "删除水印表数据", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteBaseWatermark([FromRoute] string id)
         => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteBaseWatermarkCmd(id)));

        /// <summary>
        /// 按ID但条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("BasewatermarkById/{id}")]
        [Log(Title = "按ID查找水印表数据", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindBaseWatermarkById([FromRoute] string id)
        => ObjectResponse.Ok("ok", await mediator.Send(new FindBaseWatermarkByIdQry(id)));

        /// <summary>
        /// 水印管理列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="qry"></param>
        /// <returns></returns>
        [HttpPost, Route("BasewatermarkPageList/{pageindex}/{pagesize}")]
        [Log(Title = "水印管理列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> BasewatermarkPageList([FromRoute] int pageindex, [FromRoute] int pagesize, [FromBody] FindBaseWatermarkPageListQry qry)
        {
            qry.SetPageParm(pageindex, pagesize);
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }
    }
}