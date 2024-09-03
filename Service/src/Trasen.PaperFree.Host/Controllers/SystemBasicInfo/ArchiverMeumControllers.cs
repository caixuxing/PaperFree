using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ArchiverMeumCmd;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ArchiverMeumQry;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 归档菜单目录列表
    /// </summary>
    [Route("api/EssentialTreeStructure")]
    [ApiController]
    public class ArchiverMeumControllers : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// 按机构加载结构目录树
        /// </summary>
        /// <param name="mediator"></param>
        public ArchiverMeumControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// 归档菜单目录列表
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        [HttpPost, Route("GetTreeDate")]
        [Log(Title = "归档菜单目录列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> ArchiverMeumList([FromBody] FindArchiverMeumListQry qry)
        {
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }

        /// <summary>
        /// 创建归档菜单目录表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateTreeDate")]
        [Log(Title = "创建归档菜单目录表数据", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateBaseWatermar([FromBody] CreateArchiverMeumCmd request)
         => ObjectResponse.Ok("创建成功", await mediator.Send(request));

        /// <summary>
        /// 修改归档菜单目录表数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateTreeDate/{id}")]
        [Log(Title = "修改归档菜单目录表数据", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateBaseWatermark([FromRoute] string id, [FromBody] ModifyArchiverMeumCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        /// <summary>
        /// 删除归档菜单目录表数据
        /// </summary>
        /// <param name="id">归档菜单目录数据ID</param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteTreeDate/{id}")]
        [Log(Title = "删除归档菜单目录表数据", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteBaseWatermark([FromRoute] string id)
         => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteArchiverMeumCmd(id)));

        /// <summary>
        /// 按ID单条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("BaseTreeDateById/{id}")]
        [Log(Title = "按ID查找归档菜单目录表数据", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindBaseWatermarkById([FromRoute] string id)
        => ObjectResponse.Ok("ok", await mediator.Send(new FindArchiverMeumByIdQry(id)));
    }
}