using System.ComponentModel.DataAnnotations;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.EssentialDocument
{
    /// <summary>
    /// 批量构建仓库指令
    /// </summary>
    /// <param name="cmd"></param>
    public record CreateEssentialDocumentsListCmd(CreateEssentialDocumentsCmd cmd) : IRequest<bool>;
    public record CreateEssentialDocumentsCmd : IRequest<string>
    {
        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string DeptCode { get; set; }
        /// <summary>
        /// 目录编码
        /// </summary>
        [Required]
        public string FatherMeumid { get; set; }
        /// <summary>
        /// 目录类型（1、目录，2、节点）（当目录里子节点全选时只传父目录信息）
        /// </summary>
        [Required]
        public string MeumType { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string HospCode { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string InputCode { get; set; }
        ///// <summary>
        ///// 状态 状态（0、正常，1、作废）
        ///// </summary>
        //public string Status { get;  set; }
        [Required]
        public List<TreeDate> ListTreeDate { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderId { get; set; }
        public class CreateEssentialDocumentsValidate : AbstractValidator<CreateEssentialDocumentsCmd>
        {
            public CreateEssentialDocumentsValidate()
            {
                RuleFor(x => x.DeptCode).NotEmpty().WithMessage("科室编码不能为空！").MaximumLength(100).WithMessage("科室编码长度不能超过100个字符");
                RuleFor(x => x.FatherMeumid).NotEmpty().WithMessage("目录编码不能为空！").MaximumLength(20).WithMessage("目录编码长度不能超过20个字符");
                RuleFor(x => x.MeumType).NotEmpty().WithMessage("目录类型不能为空！").MaximumLength(10).WithMessage("录类型长度不能超过10个字符");
            }
        }
        public record TreeDate
        {
            /// <summary>
            /// 目录编码
            /// </summary>
            public string FatherMeumid { get; set; }
        }
    }
}