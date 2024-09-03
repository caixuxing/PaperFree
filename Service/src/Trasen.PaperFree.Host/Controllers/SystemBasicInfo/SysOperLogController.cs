using Trasen.PaperFree.Application.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.SysOperLog;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.Shared.Help;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.SystemBasicInfo
{
    /// <summary>
    /// 系统操作日志
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SysOperLogController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediator"></param>
        public SysOperLogController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// 系统日志分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="qry"></param>
        /// <returns></returns>
        [HttpPost, Route("PageList/{pageIndex}/{pageSize}")]
        [Log(Title = "系统日志管理列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindSysOperLogPageList([FromRoute] int pageIndex, [FromRoute] int pageSize,
            [FromBody] FindSysOperLogPageListQry qry)
        {
            qry.SetPagePram(pageIndex, pageSize);
            return ObjectResponse.Ok("ok", await _mediator.Send(qry));
        }



        /// <summary>
        /// 获取业务类型
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("BusinessTypeAll")]
        [Log(Title = "业务类型", BusinessType = BusinessType.GET)]
        public IActionResult BusinessTypeAll()=> ObjectResponse.Ok("ok", Enum.GetValues(typeof(BusinessType))
                .Cast<Enum>()
                .Select(x => new DropSelectDto<int> { Id = x.GetHashCode(), Name = x.ToDescription() })
                .ToList());


        /// <summary>
        /// 操作类型
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("BperatorTypeAll")]
        [Log(Title = "操作类型", BusinessType = BusinessType.GET)]
        public IActionResult BperatorTypeAll() => ObjectResponse.Ok("ok", Enum.GetValues(typeof(BperatorType))
                .Cast<Enum>()
                .Select(x => new DropSelectDto<int> { Id = x.GetHashCode(), Name = x.ToDescription() })
                .ToList());



        /// <summary>
        /// 日志状态
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("StatusLogTypeAll")]
        [Log(Title = "日志状态", BusinessType = BusinessType.GET)]
        public IActionResult StatusLogTypeAll() => ObjectResponse.Ok("ok", Enum.GetValues(typeof(StatusLogType))
                .Cast<Enum>()
                .Select(x => new DropSelectDto<int> { Id = x.GetHashCode(), Name = x.ToDescription() })
                .ToList());

        /// <summary>
        /// 系统日志详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet, Route("{id}")]
        [Log(Title = "系统日志详细", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindSysOperLogDetailById([FromRoute] string id)
            => ObjectResponse.Ok("ok", await _mediator.Send(new FindSysOperLogDetailByIdQry(id)));
    }
}