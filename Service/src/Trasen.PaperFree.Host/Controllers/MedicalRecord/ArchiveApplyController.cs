using System.ComponentModel;
using Trasen.PaperFree.Application.Dto;
using Trasen.PaperFree.Application.MedicalRecord.Commands.Archive;
using Trasen.PaperFree.Application.MedicalRecord.Query.Archive;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.MedicalRecord
{
    /// <summary>
    /// 归档申请流程
    /// </summary>
    [Route("api/ArchiveApply")]
    [ApiController]
    public class ArchiveApplyController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediator"></param>
        public ArchiveApplyController(IMediator mediator) => this.mediator = mediator;

        /// <summary>
        /// 病历归档申请
        /// </summary>
        /// <param name="id">档案号ID</param>
        /// <returns></returns>
        [HttpPost, Route("{id}")]
        [Log(Title = "病历归档申请", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> ArchiveApply([FromRoute] string id)
            => ObjectResponse.Ok("操作成功！", await mediator.Send(new CreateArchiveApplyCmd(id)));

        /// <summary>
        /// 删除归档申请
        /// </summary>
        /// <param name="id">归档申请ID</param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        [Log(Title = "删除归档申请", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteArchiveApply([FromRoute] string id)
            => ObjectResponse.Ok("删除成功！", await mediator.Send(new DeleteArchiveApplyCmd(id)));

        /// <summary>
        /// 归档申请管理列表
        /// </summary>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="qry">归档申请管理列表检索参入条件</param>
        /// <returns></returns>
        [HttpPost, Route("PageList/{pageIndex}/{pageSize}")]
        [Log(Title = "归档申请管理列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> ArchiveApplyPageList([FromRoute] int pageIndex, [FromRoute] int pageSize,
            [FromBody] FindArchiveApplyPageListQry qry)
            => ObjectResponse.Ok("ok", await mediator.Send(qry.SetPageParam(pageIndex, pageSize)));



        /// <summary>
        /// 审批详情信息
        /// </summary>
        /// <param name="id">归档申请ID</param>
        /// <returns></returns>
        [HttpGet, Route("ReadApproval/{id}")]
        [Log(Title = "审批详情信息", BusinessType = BusinessType.APPROVAL)]
        public async Task<IActionResult> ReadApproval([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindArchiveApplyIdQry(id)));


        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="id">病历归档申请ID</param>
        /// <param name="request">归档审批指令</param>
        /// <returns></returns>
        [HttpPost, Route("Approval/{id}")]
        [Log(Title = "归档审批", BusinessType = BusinessType.APPROVAL)]
        public async Task<IActionResult> Approval([FromRoute] string id, [FromBody] ApprovalArchiveCmd request)
            => ObjectResponse.Ok("ok", await mediator.Send(request.SetArchiveApplyId(id)));

        /// <summary>
        /// 审批时间轴
        /// </summary>
        /// <param name="id">归档申请ID</param>
        /// <returns></returns>
        [HttpPost, Route("ApprovalTimeline/{id}")]
        [Log(Title = "审批时间轴", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> Approval([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindApprovalTimelineQry(id)));



        /// <summary>
        /// 流程申请状态
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("WorkAppyStatuList")]
        [Log(Title = "流程申请状态", BusinessType = BusinessType.GET)]
        public IActionResult EventDirectionBranchDroupList()
            => ObjectResponse.Ok("ok", Enum.GetValues(typeof(ProcessStatusType))
                .Cast<Enum>()
                .Select(x => new DropSelectDto<int> { Id = x.GetHashCode(), Name = x.ToDescription() })
                .ToList());

    }
}