using Microsoft.International.Converters.PinYinConverter;
using System.Text.RegularExpressions;
using Trasen.PaperFree.Application.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.CustomException;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.Shared.Help;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 流程设计
    /// </summary>
    [Route("api/ProcessDesign")]
    [ApiController]
    public class ProcessDesignController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediator"></param>
        public ProcessDesignController(IMediator mediator) => this.mediator = mediator;

        /// <summary>
        /// 创建流程设计
        /// </summary>
        /// <param name="request">创建流程设计指令</param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        [Log(Title = "创建流程设计", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreteWorkDesign([FromBody] CreateWorkDesignCmd request)
        {
            return ObjectResponse.Ok("创建成功", await mediator.Send(request));
        }

        /// <summary>
        /// 更新流程设计
        /// </summary>
        /// <param name="id">流程设计ID</param>
        /// <param name="request">更新流程设计指令</param>
        /// <returns></returns>
        [HttpPut, Route("ProcessDesign_old/{id}")]
        [Log(Title = "更新流程设计", BusinessType = BusinessType.UPDATE)]
        [Obsolete("废弃")]
        public async Task<IActionResult> UpdateProcessDesign([FromRoute] string id, [FromBody] ModifyProcessDesignCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        /// <summary>
        /// 更新流程设计
        /// </summary>
        /// <param name="id">流程设计ID</param>
        /// <param name="request">更新流程设计指令</param>
        /// <returns></returns>
        [HttpPut, Route("ProcessDesign/{id}")]
        [Log(Title = "更新流程设计", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateProcessDesign([FromRoute] string id, [FromBody] ModifyWorkDesignCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        

        /// <summary>
        ///删除流程设计
        /// </summary>
        /// <param name="id">流程设计ID</param>
        /// <returns></returns>
        [HttpDelete, Route("ProcessDesign/{id}")]
        [Log(Title = "删除流程设计", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteProcessDesign([FromRoute] string id)
            => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteProcessDesignCmd(id)));

        /// <summary>
        ///按ID查找流程设计
        /// </summary>
        /// <param name="id">流程设计ID</param>
        /// <returns></returns>
        [HttpGet, Route("ProcessDesign/{id}")]
        [Log(Title = "按ID查找流程设计", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindProcessDesignById([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindProcessDesignByIdQry(id)));

        /// <summary>
        /// 流程设计分页列表
        /// </summary>
        /// <param name="pageSize">页码大小</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="request">流程设计分页列表检索条件</param>
        /// <returns></returns>
        [HttpPost, Route("PageList/{pageSize}/{pageIndex}")]
        [Log(Title = "流程设计分页列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindProcessDesignPageList([FromRoute] int pageSize, [FromRoute] int pageIndex,
            [FromBody] FindProcessDesignPageListQry request)
        {
            request.SetPagePram(pageIndex, pageSize);
            return ObjectResponse.Ok("ok", await mediator.Send(request));
        }

        /// <summary>
        /// 创建流程节点
        /// </summary>
        /// <param name="request">创建流程节点指令</param>
        /// <returns></returns>
        [HttpPost, Route("ProcessNode")]
        [Log(Title = "创建流程节点", BusinessType = BusinessType.ADD)]
        [Obsolete("废弃")]
        public async Task<IActionResult> CreteProcessNode([FromBody] CreateProcessNodeCmd request)
            => ObjectResponse.Ok("创建成功", await mediator.Send(request));

        /// <summary>
        /// 更新流程节点
        /// </summary>
        /// <param name="id">流程节点ID</param>
        /// <param name="request">更新流程节点指令</param>
        /// <returns></returns>
        [HttpPut, Route("ProcessNode/{id}")]
        [Log(Title = "更新流程节点", BusinessType = BusinessType.UPDATE)]
        [Obsolete("废弃")]
        public async Task<IActionResult> UpdateProcessNode([FromRoute] string id, [FromBody] ModifyProcessNodeCmd request)
        {
            request.SetId(id);
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }

        /// <summary>
        ///删除流程节点
        /// </summary>
        /// <param name="id">流程节点ID</param>
        /// <returns></returns>
        [HttpDelete, Route("ProcessNode/{id}")]
        [Log(Title = "删除流程节点", BusinessType = BusinessType.DELETE)]
        [Obsolete("废弃")]
        public async Task<IActionResult> DeleteProcessNode([FromRoute] string id)
            => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteProcessNodeCmd(id)));

        /// <summary>
        ///按ID查找流程节点
        /// </summary>
        /// <param name="id">流程节点ID</param>
        /// <returns></returns>
        [HttpGet, Route("ProcessNodeById/{id}")]
        [Log(Title = "按ID查找流程节点信息", BusinessType = BusinessType.GET)]
        [Obsolete("废弃")]
        public async Task<IActionResult> FindProcessNodeById([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindProcessNodeByIdQry(id)));

        /// <summary>
        /// 流程节点分页列表
        /// </summary>
        /// <param name="pageSize">页码大小</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="id">流程设计ID</param>
        /// <returns></returns>
        [HttpGet, Route("NodePageList/{pageSize}/{pageIndex}/{id}")]
        [Log(Title = "流程节点分页列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindNodePageList([FromRoute] int pageSize, [FromRoute] int pageIndex, [FromRoute] string id)
        {
            var request = new FindProcessNodePageListQry();
            request.ProcessDesignId = id;
            request.SetPagePram(pageIndex, pageSize);
            return ObjectResponse.Ok("ok", await mediator.Send(request));
        }

        /// <summary>
        /// 节点事件方向分支
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("EventDirectionBranch")]
        [Log(Title = "节点事件方向分支", BusinessType = BusinessType.GET)]
        public IActionResult EventDirectionBranchDroupList()
        {
            var eventDirectionType = EnumberHelper.GetEnumSortType<EventDirectionType>().OrderBy(i => i.Sort).Select(x => new DropSelectDto<int>
            {
                Id = x.EnumValue,
                Name = x.Desction
            }).AsEnumerable();
            return ObjectResponse.Ok("ok", eventDirectionType);
        }
        /// <summary>
        /// 按ID获取流程设计下流程节点下拉框集合
        /// </summary>
        /// <param name="id">流程设计ID</param>
        /// <returns></returns>
        [HttpGet, Route("DropSelectNode/{id}")]
        [Log(Title = "流程下拉选择节点集合", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> DropSelectNodeByProcessDesignId([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new FindProcessNodeByProcessDesignIdQry(id)));

        /// <summary>
        /// 流程模板类型
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("WorkFlowTempType")]
        [Log(Title = "流程模板类型", BusinessType = BusinessType.GET)]
        public IActionResult WorkFlowTempType()
            => ObjectResponse.Ok("ok", Enum.GetValues(typeof(ProcessTempType))
                .Cast<Enum>()
                .Select(x => new DropSelectDto<int> { Id = x.GetHashCode(), Name = x.ToDescription() })
                .ToList());

        /// <summary>
        /// 按流程模板类型获取流程状态下拉框集合
        /// </summary>
        /// <param name="type">流程模板类型</param>
        /// <returns></returns>
        [HttpGet, Route("WorkFlowStatusList/{type}")]
        [Log(Title = "流程状态下拉框集合", BusinessType = BusinessType.GET)]
        public IActionResult WorkFlowTempType([FromRoute] ProcessTempType type)
        {
            var Types = EnumberHelper.GetEnumType<WorkFlowState>()
                .Where(i => i.ParentId == (int)type)
                 .Select(x => new DropSelectDto<int>
                 {
                     Id = x.EnumValue,
                     Name = x.Desction
                 })
                 .OrderBy(x => x.Id)
                 .ToList();
            return ObjectResponse.Ok("ok", Types);
        }
        /// <summary>
        /// 流程状态下拉框集合全部
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("WorkFlowStatusListAll")]
        [Log(Title = "流程状态下拉框集合", BusinessType = BusinessType.GET)]
        public IActionResult WorkFlowTempTypeAll()
        {
            var Types = EnumberHelper.GetEnumType<WorkFlowState>()
                 .Select(x => new DropSelectDto<int>
                 {
                     Id = x.EnumValue,
                     Name = x.Desction
                 })
                 .OrderBy(x => x.Id)
                 .ToList();
            return ObjectResponse.Ok("ok", Types);
        }

        /// <summary>
        /// 复制流程模板
        /// </summary>
        /// <param name="id">流程设计ID</param>
        /// <returns></returns>
        [HttpPost, Route("CopyWorkFlowTemp/{id}")]
        [Log(Title = "复制流程模板", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CopyWorkFlowTemp([FromRoute] string id)
            => ObjectResponse.Ok("ok", await mediator.Send(new CopyProcessDesignCmd(id)));
    }
}