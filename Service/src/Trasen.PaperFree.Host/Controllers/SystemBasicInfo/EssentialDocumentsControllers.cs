using Trasen.PaperFree.Application.SystemBasicInfo.Commands.EssentialDocument;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.EssentialDocument;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 必传文件配置
    /// </summary>
   // [Route("api/EssentialDocument")]
  //  [ApiController]
    public class EssentialDocumentsControllers : ControllerBase
    {
        private readonly IMediator mediator;

        public EssentialDocumentsControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #region
        /// <summary>
        ///// 新增必传文件配置
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost, Route("CreateEssentialDocuments")]
        //    [Log(Title = "新增必传文件配置", BusinessType = BusinessType.ADD)]
        //    public async Task<IActionResult> CreateBaseWatermar([FromBody] CreateEssentialDocumentsCmd request)
        //     => ObjectResponse.Ok("创建成功", await mediator.Send(request));
        ///// <summary>
        ///// 修改必传文件配置
        ///// </summary>
        ///// <param name="id">水印ID</param>
        ///// <param name="request">修改内容</param>
        ///// <returns></returns>
        //[HttpPut, Route("UpdateEssentialDocuments/{id}")]
        //[Log(Title = "修改必传文件配置", BusinessType = BusinessType.UPDATE)]
        //public async Task<IActionResult> UpdateBaseWatermark([FromRoute] string id, [FromBody] ModifyEssentialDocumentsCmd request)
        //{
        //    request.SetId(id);
        //    return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        //}
        #endregion

        /// <summary>
        /// 删除必传文件配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteEssentialDocuments/{id}")]
        [Log(Title = "删除必传文件配置", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteBaseWatermark([FromRoute] string id)
             => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteEssentialDocumentsCmd(id)));

        /// <summary>
        /// 按ID查找必传文件配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("EssentialDocumentsById/{id}")]
        [Log(Title = "按ID查找必传文件配置", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindBaseWatermarkById([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindEssentialDocumentByIdQry(id)));

        /// <summary>
        /// 传文件配置管理列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="qry"></param>
        /// <returns></returns>
        [HttpPost, Route("EssentialDocumentPageList/{pageindex}/{pagesize}")]
        [Log(Title = "传文件配置管理列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> EssentialDocumentPageList([FromRoute] int pageindex, [FromRoute] int pagesize, [FromBody] FindEssentialDocumentPageListQry qry)
        {
            qry.SetPageParm(pageindex, pagesize);
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }

        /// <summary>
        /// 批量新增必传文件配置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateEssentialDocumentsList")]
        [Log(Title = "批量新增必传文件配置", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateBaseWatermarList([FromBody] CreateEssentialDocumentsCmd request)
        {
            var data = new CreateEssentialDocumentsListCmd(request);
            return ObjectResponse.Ok("创建成功", await mediator.Send(data));
        }

        /// <summary>
        /// 批量修改必传文件配置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateEssentialDocuments")]
        [Log(Title = "保存更新必传文件配置", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateBaseWatermarkList([FromBody] ModifyEssentialDocumentsCmd request)
        {
            var data = new ModifyEssentialDocumentsListCmd(request);
            return ObjectResponse.Ok("更新成功", await mediator.Send(data));
        }
    }
}