using System.ComponentModel.DataAnnotations;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.EssentialDocument
{
    /// <summary>
    /// 必传文件批量构建修改指令
    /// </summary>
    /// <param name="cmd"></param>
    public record ModifyEssentialDocumentsListCmd(ModifyEssentialDocumentsCmd cmd) : IRequest<bool>;
    public record ModifyEssentialDocumentsCmd : IRequest<bool>
    {
        //public string Id { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string DeptCode { get; set; }
        /// <summary>
        /// 目录类型（1、目录，2、节点）（当目录里子节点全选时只传父目录信息）
        /// </summary>
        [Required]
        public string MeumType { get; set; }
        /// <summary>
        /// 状态 状态（0、正常，1、作废）
        /// </summary>
        [Required]
        public string Status { get; set; }
        /// <summary>
        /// 排序ID
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get; set; }
        public List<UpdateTreeDatelist> ListTreeDate { get; set; }
    }

    /// <summary>
    /// 验证规则
    /// </summary>
    public class ModifyEssentialDocumentstValidate : AbstractValidator<ModifyEssentialDocumentsCmd>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ModifyEssentialDocumentstValidate()
        {
            RuleFor(x => x.DeptCode).NotEmpty().WithMessage("科室编码不能为空！").MaximumLength(20).WithMessage("科室编码长度不能超过20个字符");
            RuleFor(x => x.MeumType).NotEmpty().WithMessage("目录类型不能为空！").MaximumLength(10).WithMessage("录类型长度不能超过10个字符");
            RuleFor(x => x.Status).NotEmpty().WithMessage("状态不能为空！");
        }
    }

    public record UpdateTreeDatelist
    {
        public string Id { get; set; }
        /// <summary>
        /// 目录编码
        /// </summary>
        [Required]
        public string FatherMeumid { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        [Required]
        public bool ChckeType { get; set; }

        public class ModifyUpdateTreeDatelistValidate : AbstractValidator<UpdateTreeDatelist>
        {
            public ModifyUpdateTreeDatelistValidate()
            {
                RuleFor(x => x.FatherMeumid).NotEmpty().WithMessage("目录编码不能为空！").MaximumLength(20).WithMessage("目录编码长度不能超过20个字符");
            }
        }
    }
}