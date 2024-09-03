namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseCopyModedetail
{
    public record CreateBaseCopyModedetailCmd : IRequest<string>
    {
        /// <summary>
        /// 目录id
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 状态（有效，无效） 8
        /// </summary>
        public string Status { get; set; }
        public class CreateBaseCopyModedetailValidate : AbstractValidator<CreateBaseCopyModedetailCmd>
        {
            public CreateBaseCopyModedetailValidate()
            {
                RuleFor(x => x.MenuId).NotEmpty().WithMessage("模板ID不能为空！")
                    .MaximumLength(30).WithMessage("模板名称长度不能超过30个字符");
                RuleFor(x => x.Status).NotEmpty().WithMessage("模板状态不能为空！")
                  .MaximumLength(30).WithMessage("模板状态长度不能超过20个字符");
            }
        }
    }
}