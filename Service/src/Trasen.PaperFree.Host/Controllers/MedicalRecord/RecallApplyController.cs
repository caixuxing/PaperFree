using Trasen.PaperFree.Application.MedicalRecord.Commands.Archive;
using Trasen.PaperFree.Application.MedicalRecord.Commands.Recall;
using Trasen.PaperFree.Application.MedicalRecord.Query.Archive;
using Trasen.PaperFree.Application.MedicalRecord.Query.Recall;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.MedicalRecord
{
    /// <summary>
    /// 召回申请流程
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecallApplyController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediator"></param>
        public RecallApplyController(IMediator mediator) => this.mediator = mediator;

        /// <summary>
        /// 病历召回申请
        /// </summary>
        /// <param name="id">档案号id</param>
        /// <returns></returns>
        [HttpPost, Route("{id}")]
        [Log(Title = "病历召回申请", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> ArchiveApply([FromRoute] string id)
        {
            return ObjectResponse.Ok("操作成功！", await mediator.Send(new CreateRecallApplyCmd(id)));
        }
 

        /// <summary>
        /// 删除召回申请
        /// </summary>
        /// <param name="id">召回申请ID</param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        [Log(Title = "删除召回申请", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteArchiveApply([FromRoute] string id)
            => ObjectResponse.Ok("删除成功！", await mediator.Send(new DeleteRecallApplyCmd(id)));

        /// <summary>
        /// 召回申请管理列表
        /// </summary>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="qry">列表检索入参</param>
        /// <returns></returns>
        [HttpPost, Route("PageList/{pageIndex}/{pageSize}")]
        [Log(Title = "召回申请管理列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> ArchiveApplyPageList([FromRoute] int pageIndex, [FromRoute] int pageSize,
            [FromBody] FindRecallApplyPageListQry qry)
            => ObjectResponse.Ok("ok", await mediator.Send(qry.SetPageParam(pageIndex, pageSize)));

        /// <summary>
        /// 召回审批
        /// </summary>
        /// <param name="id">召回申请ID</param>
        /// <param name="request">审批入参</param>
        /// <returns></returns>
        [HttpPost, Route("Approval/{id}")]
        [Log(Title = "召回审批", BusinessType = BusinessType.APPROVAL)]
        public async Task<IActionResult> Approval([FromRoute] string id, [FromBody] ApprovalRecallCmd request)
            => ObjectResponse.Ok("ok", await mediator.Send(request.SetArchiveApplyId(id)));



        /// <summary>
        /// 召回审批详情信息
        /// </summary>
        /// <param name="id">召回申请ID</param>
        /// <returns></returns>
        [HttpGet, Route("ReadApproval/{id}")]
        [Log(Title = "召回审批详情信息", BusinessType = BusinessType.APPROVAL)]
        public async Task<IActionResult> ReadApproval([FromRoute] string id) 
            => ObjectResponse.Ok("ok", await mediator.Send(new FindRecallApplyIdQry(id)));

        /// <summary>
        /// 召回审批时间轴
        /// </summary>
        /// <param name="id">召回申请ID</param>
        /// <returns></returns>
        [HttpPost, Route("ApprovalTimeline/{id}")]
        [Log(Title = "召回审批时间轴", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> Approval([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindRecallApprovalTimelineQry(id)));
    }
}