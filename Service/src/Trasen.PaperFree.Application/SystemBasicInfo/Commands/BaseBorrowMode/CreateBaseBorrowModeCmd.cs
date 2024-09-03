namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseBorrowMode
{
    public record CreateBaseBorrowModeCmd : IRequest<string>
    {
        /// 模板名称
        /// </summary>
        public string ModeName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public string IsEnable { get; set; }
        public class CreateBaseBorrowModeValidate : AbstractValidator<CreateBaseBorrowModeCmd>
        {
            public CreateBaseBorrowModeValidate()
            {
                RuleFor(x => x.ModeName).NotEmpty().WithMessage("模板名称不能为空！")
                    .MaximumLength(20).WithMessage("模板名称长度不能超过20个字符");
                RuleFor(x => x.DeptCode).NotEmpty().WithMessage("科室编码不能为空！")
                   .MaximumLength(100).WithMessage("科室编码长度不能超过100个字符");
            }
        }
    }
}