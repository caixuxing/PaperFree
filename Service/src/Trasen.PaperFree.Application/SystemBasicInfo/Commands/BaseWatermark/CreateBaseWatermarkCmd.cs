namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseWatermark
{
    public record CreateBaseWatermarkCmd : IRequest<string>
    {
        ///// <summary>
        ///// 水印id
        ///// </summary>
        //public string WatermarkId { get;  set; }
        /// <summary>
        /// 水印名称
        /// </summary>
        public string WatermarkName { get; set; }
        /// <summary>
        /// 使用场景
        /// </summary>
        public string UseScene { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// x坐标
        /// </summary>
        public string Xstation { get; set; }
        /// <summary>
        /// y坐标
        /// </summary>
        public string  Ystation { get; set; }
        /// <summary>
        /// 角度
        /// </summary>
        public string Angle { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string Font { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public string FontSize {  get; set; }
        /// <summary>
        /// 透明度
        /// </summary>
        public int Pellucidity { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public string Hight { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string Width { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// PicX
        /// </summary>
        public string PicX { get; set; }
        /// <summary>
        /// PicY
        /// </summary>
        public string PicY { get; set; }
        /// <summary>
        /// 左右间距
        /// </summary>
        public string GapX { get; set; }
        /// <summary>
        /// 上下间距
        /// </summary>
        public string GapY { get; set; }
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

        public class CreateBaseWatermarkValidate : AbstractValidator<CreateBaseWatermarkCmd>
        {
            public CreateBaseWatermarkValidate()
            {
                RuleFor(x => x.UseScene).NotEmpty().WithMessage("使用场景不能为空！")
                   .MaximumLength(20).WithMessage("使用场景长度不能超过20个字符！");
                RuleFor(x => x.Color).NotEmpty().WithMessage("颜色不能为空！")
                  .MaximumLength(100).WithMessage("颜色长度不能超过100个字符");
                RuleFor(x => x.WatermarkName).NotEmpty().WithMessage("水印名称不能为空！")
                 .MaximumLength(100).WithMessage("水印名称长度不能超过100个字符");

                RuleFor(x => x.Xstation).MaximumLength(3).WithMessage("文字X坐标不能超过3个字符");
                RuleFor(x => x.Ystation).MaximumLength(3).WithMessage("文字Y坐标不能超过3个字符");
                RuleFor(x => x.Angle).NotEmpty().WithMessage("文字角度不能为空！").MaximumLength(3).WithMessage("文字角度长度不能超过3");
                RuleFor(x => x.Direction).NotEmpty().WithMessage("文字方向不能为空！").MaximumLength(10).WithMessage("文字方向长度不能超过10个字符"); ;
                RuleFor(x => x.Font).NotEmpty().WithMessage("字体不能为空！").MaximumLength(10).WithMessage("字体长度不能大于20个字符！");
                RuleFor(x => x.Hight).NotEmpty().MaximumLength(3).WithMessage("高度长度不能超过3个字符").NotEmpty().WithMessage("高度不能为空！");
                RuleFor(x => x.Width).NotEmpty().MaximumLength(3).WithMessage("高度长度不能超过3个字符").NotEmpty().WithMessage("宽度不能为空！");

                RuleFor(x => x.PicX).MaximumLength(10).WithMessage("PicX长度不能超过20个字符！");
                RuleFor(x => x.PicY).MaximumLength(10).WithMessage("PicY长度不能超过20个字符！");
                RuleFor(x => x.OrgCode).MaximumLength(50).WithMessage("机构编码长度不能超过").NotEmpty().WithMessage("机构编码不能为空");
                RuleFor(x => x.HospCode).MaximumLength(50).WithMessage("院区编码长度不能超过").NotEmpty().WithMessage("院区编码不能为空");
                RuleFor(x => x.InputCode).MaximumLength(50).WithMessage("辖区编码长度不能超过").NotEmpty().WithMessage("辖区编码不能为空");
                RuleFor(x => x.GapX).NotEmpty().WithMessage("左右间距不能为空！").MaximumLength(10).WithMessage("左右间距长度不能超过10个字符");
                RuleFor(x => x.GapY).NotEmpty().WithMessage("上下间距不能为空！").MaximumLength(10).WithMessage("上下间距长度不能超过10个字符");
            }
            private bool BeANumber(int value)
            {
                return int.TryParse(value.ToString(), out _);
            }
        }
    }
}