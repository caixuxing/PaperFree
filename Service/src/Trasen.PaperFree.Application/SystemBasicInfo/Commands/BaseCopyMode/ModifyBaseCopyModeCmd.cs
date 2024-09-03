namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseCopyMode
{
    internal class ModifyBaseCopyModeCmd : IRequest<bool>
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

        public string Id { get; private set; }

        public ModifyBaseCopyModeCmd SetId(string id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 验证规则
        /// </summary>
        public class ModifyBaseCopyModeValidate : AbstractValidator<ModifyBaseCopyModeCmd>
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public ModifyBaseCopyModeValidate()
            {
                //  RuleFor(x => x.Id).NotEmpty().WithMessage("更新主键ID不能为空！");
            }
        }
    }
}