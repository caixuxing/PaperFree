using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using System.Net.Http;
using Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails;
using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Domain.FileTable.Entity;
using Trasen.PaperFree.Domain.PatientDetails.Repository;
using Trasen.PaperFree.Domain.SeedWork;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.Shared.FileConversion;
using static PdfSharpCore.Pdf.PdfDictionary;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.PatientDetails
{
    internal class FileUploadHandlers : IRequestHandler<CreateFilesOtherCmd, string>
    {
        private readonly IOutpatientInfoRepo _outpatientInfoRepo;
        private readonly IFilesOtherRepo _filesOtherRepo;
        private readonly Validate<CreateFilesOtherCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ICurrentUser currentUser;
        private readonly IGuidGenerator guidGenerator;
        private readonly IMinioFileService minioFileService;

        public FileUploadHandlers(Validate<CreateFilesOtherCmd> validate, IUnitOfWork unitOfWork, IFilesOtherRepo filesOtherRepo, IHttpClientFactory httpClientFactory, ICurrentUser currentUser, IGuidGenerator guidGenerator, IOutpatientInfoRepo outpatientInfoRepo, IMinioFileService minioFileService)
        {
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            _filesOtherRepo = filesOtherRepo;
            this.httpClientFactory = httpClientFactory;
            this.currentUser = currentUser;
            this.guidGenerator = guidGenerator;
            _outpatientInfoRepo = outpatientInfoRepo;
            //注入Minio
            this.minioFileService = minioFileService;
        }

        public async Task<string> Handle(CreateFilesOtherCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);

            //流转文件保存
            var fileName = Path.GetFileName(request.FileStreams.FileName);
            if (!request.FileSavename.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                request.FileSavename = request.FileSavename + ".PDF";
            }
            var path = Path.Combine(request.OrgCode, DateTime.Now.ToLocalTime().Year.ToString(), DateTime.Now.ToLocalTime().Month.ToString(),
                                 DateTime.Now.ToLocalTime().Day.ToString(), request.ArchiveId, request.MenuName);
            if (!Directory.Exists(Path.Combine(path)))
            {
                Directory.CreateDirectory(path);
            }
            var entity = new FilesOther(request.ArchiveId, request.MenuId, guidGenerator.Create().ToString(), fileName, request.FileSavename, request.FileType, path, WhetherType.YES
                                               , request.SourceCode);

            //var qry = await _outpatientInfoRepo.QueryAll().AnyAsync(x => x.ArchiveId == request.ArchiveId && 
            //new List<WorkFlowState>() { WorkFlowState.AWAITCOMMIT, WorkFlowState.NONE, WorkFlowState.ALREADYCOMMIT }.Contains(x.Status));
            //if (qry) throw new BusinessException(MessageType.Error, "上传失败!", "当前病历状态不能上传病历文件,只能在：待提交、已提交状态下进行文件上传");

            await _filesOtherRepo.AddAsync(entity, cancellationToken);
        


            //[Queue("default")]
            //[DisableConcurrentExecution(timeoutInSeconds: 180)]
            byte[] file = await FileConversionClass.ConvertIFormFileToByteArray(request.FileStreams);
            var jobid = BackgroundJob.Enqueue<FileUploadHandlers>(job => job.CreateFileUpload(file, path, request.FileSavename));
         //   await CreateFileUpload(request, path, fileName);


            await unitOfWork.SaveChangesAsync();
            return jobid;// entity.Id;
        }
            public async Task CreateFileUpload(byte[] file, string path, string fileName)
        {
            if (Path.GetExtension(fileName).ToLower() == ".pdf")
            {
                using (var memoryStream = new MemoryStream())
                {
                    // 将 IByte 数组写入 MemoryStream
                    memoryStream.Write(file, 0, file.Length);

                    // 转换为 Stream
                    var stream = memoryStream;
                    await minioFileService.UploadStream(stream, Path.Combine(path), "application/pdf", fileName) ;
                }
             }
            else
            {

                using (MemoryStream pdfStream = new MemoryStream())
                {
                    PdfDocument document = new PdfDocument();
                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    using MemoryStream imageStream = new MemoryStream(file);
                    XImage image = XImage.FromStream(() => imageStream);
                    gfx.DrawImage(image, 0, 0);
                    document.Save(pdfStream, false);
                    document.Dispose();
                    byte[] data = new byte[pdfStream.Length];
                    pdfStream.Read(data, 0, data.Length);
                    var base64Str = Convert.ToBase64String(data);

                    await minioFileService.UploadStream(pdfStream, Path.Combine(path), "application/pdf", Path.GetFileNameWithoutExtension(path)+".PDF");
                }
            }
        }
    }
}