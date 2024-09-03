using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.PatientDetails
{
    public record FileStreamsByIdQry : IRequest<byte[]>
    {
        /// <summary>
        /// 文件存储路径
        /// </summary>
        [Required]
        public string FilePath { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [Required]
        public string FileSavename { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode {  get; set; }

        public class FileStreamsByIdValidate : AbstractValidator<FileStreamsByIdQry>
        {
            public FileStreamsByIdValidate()
            {
                RuleFor(x => x.FilePath).NotEmpty().WithMessage("文件存储路径不能为空！");
                RuleFor(x => x.FileSavename).NotEmpty().WithMessage("文件名称不能为空");
                RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空").MaximumLength(20).WithMessage("机构编码长度不能大于20");
            }
        }
    }
}