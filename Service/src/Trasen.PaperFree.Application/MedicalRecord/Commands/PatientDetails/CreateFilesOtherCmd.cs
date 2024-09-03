using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails
{
    public record CreateFilesOtherCmd : IRequest<string>
    {
        /// <summary>
        /// 档案号ID 
        /// </summary>
        [Required]
        public string ArchiveId { get; set; }
        /// <summary>
        /// 病历目录表ID 
        /// </summary>
        [Required]
        public string MenuId { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>
        [Required]
        public string MenuName { get; set; }
        ///// <summary>
        ///// 文件唯-ID 
        ///// </summary>
        //[Required]
        //public string FileId { get; set; }
        /// <summary>
        /// 文件保存后名称 
        /// </summary>
        [Required]
        public string FileSavename { get; set; }
        /// <summary>
        /// 文件类型格式 
        /// </summary>
        public string FileType { get; set; }
        /// <summary>Z
        /// 文件流
        /// </summary>
        [Required]
        public IFormFile FileStreams { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 文件来源
        /// </summary>
        [Required]
        public string SourceCode { get; set; }
        /// <summary>
        /// 验证规则
        /// </summary>
        public class CreateFilesOtherValidate : AbstractValidator<CreateFilesOtherCmd>
        {
            public CreateFilesOtherValidate()
            {
                RuleFor(x => x.ArchiveId).NotEmpty().WithMessage("档案号不能为空！").MaximumLength(50).WithMessage("档案号不能超过50");
                RuleFor(x => x.MenuId).NotEmpty().WithMessage("病历目录表ID不能为空！").MaximumLength(50).WithMessage("病历目录表ID不能超过50");
                RuleFor(x => x.FileSavename).NotEmpty().WithMessage("文件保存后名称不能为空！").MaximumLength(200).WithMessage("文件保存后名称不能超过200");
                RuleFor(x => x.FileType).NotEmpty().WithMessage("文件类型不能为空！").MaximumLength(10).WithMessage("机构编码不能超过50");
                RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空！").MaximumLength(50).WithMessage("机构编码不能超过50");
                RuleFor(x => x.SourceCode).NotEmpty().WithMessage("文件来源不能为空！").MaximumLength(10).WithMessage("文件来源不能超过10");
                RuleFor(x => x.FileStreams).NotEmpty().WithMessage("文件流不能为空！");
            }
        }
    }
}