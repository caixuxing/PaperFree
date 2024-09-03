using Trasen.PaperFree.Application.SystemBasicInfo.Commands.CaseManagement;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.CaseManagement;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 纸质病例存储管理
    /// </summary>
    [Route("api/CaseShelfManagement")]
    [ApiController]
    public class CaseShelfControllers : ControllerBase
    {
        private readonly IMediator mediator;
        /// <summary>
        /// 注入构建
        /// </summary>
        /// <param name="mediator"></param>
        public CaseShelfControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #region// 仓库增删改

        /// <summary>
        ///创建仓库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CaseShelf")]
        [Log(Title = "创建仓库", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateCaseShelfManagement([FromBody] CreateCaseShelfManagementCmd request)
            => ObjectResponse.Ok("创建成功", await mediator.Send(request));

        /// <summary>
        /// 修改仓库内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("CaseShelf/{id}")]
        [Log(Title = "修改仓库内容", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateCaseShelfManagement([FromRoute] string id, [FromBody] ModifyCaseShelfManagementCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        /// <summary>
        ///删除仓库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("CaseShelf/{id}")]
        [Log(Title = "删除仓库", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteCaseShelfManagement([FromRoute] string id)
            => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteCaseShelfManagementCmd(id)));

        /// <summary>
        ///按ID查找仓库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("ShelfManagementById/{id}")]
        [Log(Title = "按ID查找仓库信息", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindCaseShelfManagementById([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindCaseShelfManagementByIdQry(id)));

        /// <summary>
        /// 仓库信息管理列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="qry"></param>
        /// <returns></returns>
        [HttpPost, Route("ShelfManagementPageList/{pageindex}/{pagesize}")]
        [Log(Title = "仓库信息管理列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> ShelfManagementPageList([FromRoute] int pageindex, [FromRoute] int pagesize, [FromBody] FindCaseShelfManagementPageListQry qry)
        {
            qry.SetPageParm(pageindex, pagesize);
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }

        #endregion
    }
}