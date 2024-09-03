namespace Trasen.PaperFree.Application.MedicalRecord.Commands.AnNotation
{
    public class ModifyAnNotationTableCmd : IRequest<bool>
    {
        /// <summary>
        /// 批注内容 批注内容
        /// </summary>
        public string AnNotAtIOn { get; set; }

        public DateTime? AnNotAtIOnDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 评分
        /// </summary>
        public string Lower { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string Id { get; private set; }

        public ModifyAnNotationTableCmd SetId(string id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 验证规则
        /// </summary>
        public class ModifyAnNotationTableValidate : AbstractValidator<ModifyAnNotationTableCmd>
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public ModifyAnNotationTableValidate()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("更新主键ID不能为空！");
                RuleFor(x => x.AnNotAtIOn).NotEmpty().WithMessage("批注内容不能为空！").MaximumLength(50).WithMessage("批注内容不能超过1000");
                RuleFor(x => x.Lower).MaximumLength(50).WithMessage("评分不能超过20");
                RuleFor(x => x.Remark).MaximumLength(50).WithMessage("批注内容不能超过200");
            }
        }
    }
}