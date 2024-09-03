using Trasen.PaperFree.Application.MedicalRecord.Commands.AnNotation;
using Trasen.PaperFree.Application.MedicalRecord.Query.AnNotation;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.MedicalRecord
{
    /// <summary>
    /// 备注
    /// </summary>
    [Route("api/AnNotation")]
    [ApiController]
    public class AnNotationController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediator"></param>
        public AnNotationController(IMediator mediator) => this.mediator = mediator;

        /// <summary>
        ///创建批注
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("AnNotation")]
        [Log(Title = "创建批注", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateAnNotation([FromBody] CreateAnnotationTableCmd request)
            => ObjectResponse.Ok("创建成功", await mediator.Send(request));

        /// <summary>
        /// 修改批注内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        [Log(Title = "修改批注内容", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateAnNotation([FromRoute] string id, [FromBody] ModifyAnNotationTableCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        /// <summary>
        ///批注仓库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        [Log(Title = "批注仓库", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteAnNotation([FromRoute] string id)
            => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteAnNotationTableCmd(id)));

        /// <summary>
        ///按ID查找批注信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [Log(Title = "按ID查找批注信息", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindAnNotation([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindAnNotationByIdQry(id)));
    }
}