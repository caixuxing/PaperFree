namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseBorrowMode
{
    public class ModifyBaseBorrowModeCmd : IRequest<bool>
    {
        /// <summary>
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

        public string Id { get; private set; }

        public ModifyBaseBorrowModeCmd SetId(string id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 验证规则
        /// </summary>
        public class ModifyBaseBorrowModeValidate : AbstractValidator<ModifyBaseBorrowModeCmd>
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public ModifyBaseBorrowModeValidate()
            {
                //  RuleFor(x => x.Id).NotEmpty().WithMessage("更新主键ID不能为空！");
            }
        }
    }
}