using Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails;
using Trasen.PaperFree.Application.MedicalRecord.Query.PatientDetails;
using Trasen.PaperFree.Domain.Shared.FileConversion;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.PatientDetails
{
    internal class FindPatientDetailsByIdHandlers : IRequestHandler<FileStreamsByIdQry, byte[]>
    {
        private readonly Validate<FileStreamsByIdQry> validate;
        private readonly IMinioFileService minioFileService;

        public FindPatientDetailsByIdHandlers(Validate<FileStreamsByIdQry> validate, IMinioFileService miniofileservice)
        {
            this.validate = validate;
            this.minioFileService = miniofileservice;
        }

        /// <summary>
        /// 返回文件流
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> Handle(FileStreamsByIdQry request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            if (!request.FileSavename.EndsWith(".pdf", StringComparison.CurrentCultureIgnoreCase)) {

                request.FileSavename = $"{request.FileSavename}.PDF";
            }
            return 
                await FileConversionClass.ReadFileToByteAsync(Path.Combine(Directory.GetCurrentDirectory(), request.FilePath, request.FileSavename));
        }
    }
}