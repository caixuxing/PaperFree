namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseCopyModedetail
{
    public class ModifyBaseCopyModedetailCmd : IRequest<bool>
    {
        /// <summary>
        /// 模板id
        /// </summary>
        public string ModeId { get; private set; }

        /// <summary>
        /// 目录id
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 状态（有效，无效） 8
        /// </summary>
        public string Status { get; set; }

        public ModifyBaseCopyModedetailCmd SetId(string id)
        {
            ModeId = id;
            return this;
        }

        /// <summary>
        /// 验证规则
        /// </summary>
        public class ModifyBaseCopyModedetailValidate : AbstractValidator<ModifyBaseCopyModedetailCmd>
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public ModifyBaseCopyModedetailValidate()
            {
                //  RuleFor(x => x.Id).NotEmpty().WithMessage("更新主键ID不能为空！");
            }
        }
    }
}