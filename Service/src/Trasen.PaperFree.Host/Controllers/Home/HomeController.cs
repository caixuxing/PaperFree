using Trasen.PaperFree.Application.Home.Query;
using Trasen.PaperFree.Application.MedicalRecord.Query.OutpatientInfo;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.Home
{
    /// <summary>
    /// 首页
    /// </summary>
    [Route("api/Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediator"></param>
        public HomeController(IMediator mediator) => this.mediator = mediator;
        /// <summary>
        ///头部数据统计结果集
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("HomeTotal")]
        [Log(Title = "头部数据统计结果集", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> HomeTotal( [FromBody] FindTotalQueryQry qry)
        {
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }
        /// <summary>
        /// 工作量统计
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("HomeCaseload")]
        [Log(Title = "工作量统计", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> HomeCaseload([FromBody] FindCaseloadQry qry)
        {
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }
        /// <summary>
        /// 病案统计统计
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("HomeMedicalRecordStat")]
        [Log(Title = "病案统计统计", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> HomeMedicalRecordStat([FromBody] FindMedicalRecordStatsQry qry)
        {
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }
    }
}
