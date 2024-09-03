namespace Trasen.PaperFree.Application.MedicalRecord.Commands.AnNotation
{
    public record CreateAnnotationTableCmd : IRequest<string>
    {
        /// <summary>
        /// 档案号 档案号
        /// </summary>
        public string Archiveid { get; set; }
        /// <summary>
        /// 文件id 文件id
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 批注内容 批注内容
        /// </summary>
        public string AnNotAtIOn { get; set; }
        /// <summary>
        /// 批注科室编码 批注科室编码
        /// </summary>
        public string Deptcode { get; set; }

        public DateTime? AnNotAtIOnDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public string Lower { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建人编号
        /// </summary>
        public string CreatorId { get; set; }
    }

    /// <summary>
    /// 验证规则
    /// </summary>
    public class CreateAnnotationTableValidate : AbstractValidator<CreateAnnotationTableCmd>
    {
        public CreateAnnotationTableValidate()
        {
            RuleFor(x => x.Archiveid).NotEmpty().WithMessage("档案号不能为空！").MaximumLength(50).WithMessage("档案号不能超过50");
            RuleFor(x => x.FileId).NotEmpty().WithMessage("文件id不能为空！").MaximumLength(50).WithMessage("文件id不能超过50");
            RuleFor(x => x.AnNotAtIOn).NotEmpty().WithMessage("批注内容不能为空！").MaximumLength(50).WithMessage("批注内容不能超过1000");
            RuleFor(x => x.Deptcode).NotEmpty().WithMessage("批注科室编码不能为空！").MaximumLength(100).WithMessage("批注科室不能超过100");
            RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空！").MaximumLength(50).WithMessage("机构编码不能超过50");
            RuleFor(x => x.HospCode).NotEmpty().WithMessage("院区编码不能为空！").MaximumLength(50).WithMessage("院区编码不能超过50");
            RuleFor(x => x.Lower).MaximumLength(50).WithMessage("评分不能超过20");
            RuleFor(x => x.Remark).MaximumLength(50).WithMessage("批注内容不能超过200");
        }
    }
}