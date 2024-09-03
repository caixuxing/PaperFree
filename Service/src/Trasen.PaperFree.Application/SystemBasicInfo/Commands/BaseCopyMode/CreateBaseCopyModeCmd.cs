namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseCopyMode
{
    public record CreateBaseCopyModeCmd : IRequest<string>
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string ModeName { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string InputCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Pay { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        public class CreateBaseCopyModeValidate : AbstractValidator<CreateBaseCopyModeCmd>
        {
            public CreateBaseCopyModeValidate()
            {
                RuleFor(x => x.ModeName).NotEmpty().WithMessage("模板名称不能为空！")
                    .MaximumLength(20).WithMessage("模板名称长度不能超过20个字符");
            }
        }
    }
}