namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseWatermark
{
    public class BaseWatermarkDto
    { /// <summary>
      /// 水印id
      /// </summary>
        public string Id { get; set; }

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
        public string? Xstation { get; set; }

        /// <summary>
        /// y坐标
        /// </summary>
        public string? Ystation { get; set; }

        /// <summary>
        /// 角度
        /// </summary>
        public string? Angle { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public string? Font { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// 透明度
        /// </summary>
        public int? Pellucidity { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public string? Hight { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public string? Width { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }

        ///     /// <summary>
        /// 使用机构
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// PicX
        /// </summary>
        public string PicX { get; set; }

        /// <summary>
        /// PicY
        /// </summary>
        public string PicY { get; set; }

        /// <summary>
        /// 辖区编码
        /// </summary>
        public string InputCode { get; set; }
        /// <summary>
        /// 左右间距
        /// </summary>
        public string GapX {  get; set; }
        /// <summary>
        /// 上下间距
        /// </summary>
        public string GapY {get; set; }
    }
}