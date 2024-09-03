using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Commands.Archive;

namespace Trasen.PaperFree.Application.MedicalRecord.Commands.IgnoreItme
{
    public record CreateIgnoreItmeCmd : IRequest<string>
    {
        /// <summary>
        /// 档案号
        /// </summary>
        [Required]
        public string ArchiveId { get; set; }
        /// <summary>
        /// 必传文件主键ID
        /// </summary>
        [Required]
        public string MeumTreeId { get; set; }
        public class CreateIgnoreItmeValidate : AbstractValidator<CreateIgnoreItmeCmd>
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public CreateIgnoreItmeValidate()
            {
                RuleFor(x => x.ArchiveId).NotEmpty().WithMessage("档案号不能为空！").MaximumLength(50).WithMessage("档案号长度不能大于50");
                RuleFor(x => x.MeumTreeId).NotEmpty().WithMessage("必传文件主键ID不能为空！").MaximumLength(50).WithMessage("必传文件主键ID长度不能大于50");
            }
        }
    }
}
