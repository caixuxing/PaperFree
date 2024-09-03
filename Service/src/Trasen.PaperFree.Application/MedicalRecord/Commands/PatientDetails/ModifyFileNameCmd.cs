using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails
{
    /// <summary>
    /// 文件名称修改入参
    /// </summary>
    public record ModifyFileNameCmd:IRequest<bool>
    {
        /// <summary>
        /// 文件唯一ID
        /// </summary>
        [Required]
        public string FileId {  get; set; }
        /// <summary>
        /// 修改后名称
        /// </summary>
        [Required]
        public string FILE_SAVENAME {  get; set; }
        public class ModifyFileNameValidate : AbstractValidator<ModifyFileNameCmd>
        {
            public ModifyFileNameValidate() {
                RuleFor(x=>x.FileId).NotEmpty().WithMessage("唯一ID不能为空").MaximumLength(50).WithMessage("ID长度不能大于50");
                RuleFor(x => x.FILE_SAVENAME)
                    .NotEmpty().WithMessage("修改后文件名称不能为空")
                   // .Must(Name).WithMessage("修改后文件名称中包含特殊字符")
                    .MaximumLength(200).WithMessage("修改后文件名称长度不能大于200");
            
            }
            private bool Name(string value)
            {
                // 定义正则表达式模式匹配影响文件名称的特殊字符
                Regex specialCharRegex = new Regex("[^\u4e00-\u9fa5a-zA-Z0-9]");
                return specialCharRegex.IsMatch(value);
            }
        }
    }
}
