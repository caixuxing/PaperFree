using Trasen.PaperFree.Application.SystemBasicInfo.Commands.DeptMeunTree;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.DeptMeunTree;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 必传文件配置
    /// </summary>
    [Route("api/DeptMeumTree")]
    [ApiController]
    public class DeptMenuTreeControllers : ControllerBase
    {
        private readonly IMediator mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public DeptMenuTreeControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// 批量修改必传文件配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateDeptMeumTree")]
        [Log(Title = "保存更新必传文件配置", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateDeptMeumTreeList([FromBody] ModifyDeptMeunTreeCmd request)
        {
            var data = new ModifyDeptMeunTreeCmdsListCmd(request);
            return ObjectResponse.Ok("更新成功", await mediator.Send(data));
        }

        /// <summary>
        /// 必传文件配置管理列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="qry"></param>
        /// <returns></returns>
        [HttpPost, Route("DeptMeumTreePageList/{pageindex}/{pagesize}")]
        [Log(Title = "必传文件配置管理列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> DeptMeumTreePageList([FromRoute] int pageindex, [FromRoute] int pagesize, [FromBody] FindDeptMenuTreePageListQuery qry)
        {
            qry.SetPageParm(pageindex, pagesize);
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }

        /// <summary>
        /// 必传文件目录列表
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        [HttpPost, Route("DeptMeumTreeList")]
        [Log(Title = "必传文件目录列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> DeptMeumTreeList([FromBody] FindDeptMenuTreeDeptQuery qry)
        {
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }

    }
}