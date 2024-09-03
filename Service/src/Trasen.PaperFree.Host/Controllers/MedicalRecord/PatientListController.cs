using Hangfire;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Reflection.Metadata;
using Trasen.PaperFree.Application.MedicalRecord.Commands.IgnoreItme;
using Trasen.PaperFree.Application.MedicalRecord.Commands.OutpatientInfo;
using Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails;
using Trasen.PaperFree.Application.MedicalRecord.Query.OutpatientInfo;
using Trasen.PaperFree.Application.MedicalRecord.Query.PatientDetails;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.FileConversion;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Host.Controllers.MedicalRecord
{
    /// <summary>
    /// 出院病历列表和详情
    /// </summary>
    [Route("api/PatientList")]
    [ApiController]
    public class PatientListController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMinioFileService minioFileService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="miniofileservice"></param>
        public PatientListController(IMediator mediator, IMinioFileService miniofileservice)
        {
            this.mediator = mediator;
            this.minioFileService = miniofileservice;
        }

        /// <summary>
        /// 出院病人列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("PageList/{pageIndex}/{pageSize}")]
        [Log(Title = "出院病人列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> OutPatientInfoList([FromRoute] int pageIndex, [FromRoute] int pageSize, [FromBody] FindOutpatientInfoPageListQry qry)
        {
            qry.SetPageParm(pageIndex, pageSize);
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }
        /// <summary>
        /// 在院病人列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("InPatientInfoList/{pageIndex}/{pageSize}")]
        [Log(Title = "在院病人列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> InPatientInfoList([FromRoute] int pageIndex, [FromRoute] int pageSize, [FromBody] FindInpatientInfoPageListQry qry)
        {
            qry.SetPageParm(pageIndex, pageSize);
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }
        /// <summary>
        /// 门急诊病人列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("OutpatientEmergencyPageList/{pageIndex}/{pageSize}")]
        [Log(Title = "门诊病人列表", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> OutpatientEmergencyPageList([FromRoute] int pageIndex, [FromRoute] int pageSize, [FromBody] FindOutpatientEmergencyPageListQry qry)
        {
            qry.SetPageParm(pageIndex, pageSize);
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }

        /// <summary>
        /// 高拍文件删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("FilesDelete/{id}")]
        [Log(Title = "高拍文件删除", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteBaseWatermark([FromRoute] string id)
             => ObjectResponse.Ok("删除成功", await mediator.Send(new DeleteUploadCmd(id)));

        /// <summary>
        /// /文件流返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("FileStreamsReturn")]
        [Log(Title = "文件流返回", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindBaseWatermarkById([FromBody] FileStreamsByIdQry request)
        {
            //if (!request.FileSavename.EndsWith(".pdf", StringComparison.CurrentCultureIgnoreCase))
            //    request.FileSavename = $"{request.FileSavename}.PDF";
            // = await System.IO.File.ReadAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), request.FilePath, request.FileSavename));
            byte[] fileContents = await minioFileService.DownLoad(request.FileSavename, request.FilePath);
            return File(fileContents, "application/pdf");
        }
        /// <summary>
        ///文件保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateFileStreams")]
        [Log(Title = "文件保存", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> FileStreamsAdd([FromForm] CreateFilesOtherCmd request)
        {
        
            return ObjectResponse.Ok("创建成功", await mediator.Send(request));
        }
        //
        /// <summary>
        /// 根据档案号返回患者目录
        /// </summary>
        [HttpPost, Route("FindMeumTreeArchiveId")]
        [Log(Title = "根据档案号返回患者目录", BusinessType = BusinessType.GET)]
        public async Task<IActionResult> FindMeumTreeArchiveId([FromBody] FindPatientDetailsQry qry)
        {
            return ObjectResponse.Ok("ok", await mediator.Send(qry));
        }
        /// <summary>
        /// 修改文件名称
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("ModifyFileName")]
        [Log(Title = "修改文件名称", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> ModifyFileName( [FromBody] ModifyFileNameCmd request) {
            return ObjectResponse.Ok("ok",await mediator.Send(request));
        }
        /// <summary>
        ///新增住院病人信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreatePatientInfo")]
        [Log(Title = "新增住院病人信息", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreatePatientAdd([FromBody] CreateOutpatientInfoTableCmd request)
        {
            return ObjectResponse.Ok("创建成功", await mediator.Send(request));
        }
        /// <summary>
        ///新增忽略文件信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateIgnoreItm")]
        [Log(Title = "新增忽略文件信息信息", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateIgnoreItmtAdd([FromBody] CreateIgnoreItmeCmd request)
        {
            return ObjectResponse.Ok("创建成功", await mediator.Send(request));
        }
        /// <summary>
        /// 删除忽略文件信息信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteIgnoreItm/{id}")]
        [Log(Title = "删除忽略文件信息信息", BusinessType = BusinessType.DELETE)]
        public async Task<IActionResult> DeleteIgnoreItm([FromRoute] string id)
             => ObjectResponse.Ok("删除成功", await mediator.Send(new DeletegnoreItmeCmd(id)));
        /// <summary>
        ///新增门急诊病人信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateOutpatientEmergency")]
        [Log(Title = "新增门急诊病人信息", BusinessType = BusinessType.ADD)]
        public async Task<IActionResult> CreateOutpatientEmergencyAdd([FromBody] CreateOutpatientEmergencyCmd request)
        {
            return ObjectResponse.Ok("创建成功", await mediator.Send(request));
        }
        /// <summary>
        ///修改门急诊病人信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateOutpatientEmergency")]
        [Log(Title = "修改门急诊病人信息", BusinessType = BusinessType.UPDATE)]
        public async Task<IActionResult> UpdateOutpatientEmergencyAdd([FromBody] ModifyOutpatientEmergencyCmd request)
        {
            return ObjectResponse.Ok("更新成功", await mediator.Send(request));
        }
    }
}