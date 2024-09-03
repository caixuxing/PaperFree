using System.ComponentModel.DataAnnotations.Schema;
using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 水印表
    /// </summary>
    public record BaseWatermark : FullRoot
    {
        public BaseWatermark() { }
        public BaseWatermark(string watermarkid, string watermarkname, string usescene, string color, string xstation, string ystation,
        string angle, string direction, string font, string fontsize, int pellucidity, string hight, string width, string picture, string picx, string picy, string status, string gapx, string gapy, string orgcode, string hospcode, string inputcode)
        {
            WatermarkId = watermarkid;
            WatermarkName = watermarkname;
            UseScene = usescene;
            this.Color = color;
            Xstation = xstation;
            Ystation = ystation;
            Angle = angle;
            Direction = direction;
            Font = font;
            FontSize = fontsize;
            Pellucidity = pellucidity;
            Hight = hight;
            Width = width;
            Picture = picture;
            PicX = picx;
            PicY = picy;
            GapX= gapx;
            GapY= gapy;
            Status = status;
            OrgCode = orgcode;
            HospCode = hospcode;
            InputCode = inputcode;
        }
        public BaseWatermark ChangeBaseWatermark(string watermarkid, string watermarkname, string usescene, string colour, string xstation, string ystation,
        string angle, string direction, string font,string fontsize, int pellucidity, string hight, string width, string picture, string picx, string picy, string status,string gapx, string gapy, string orgcode, string hospcode, string inputcode)
        {
            this.WatermarkId = watermarkid;
            this.WatermarkName = watermarkname;
            this.UseScene = usescene;
            this.Color = colour;
            this.Xstation = xstation;
            this.Ystation = ystation;
            this.Angle = angle;
            this.Direction = direction;
            this.Font = font;
            this.FontSize= fontsize;
            this.Pellucidity = pellucidity;
            this.Hight = hight;
            this.Width = width;
            this.Picture = picture;
            this.PicX = picx;
            this.PicY = picy;
            this.GapX = gapx;
            this.GapY = gapy;
            this.Status = status;
            this.OrgCode = orgcode;
            this.HospCode = hospcode;
            this.InputCode = inputcode;
            return this;
        }
        /// <summary>
        /// 水印id
        /// </summary>
        [NotMapped]
        public string WatermarkId { get; private set; }
        /// <summary>
        /// 水印名称
        /// </summary>
        public string? WatermarkName { get; private set; }

        /// <summary>
        /// 使用场景
        /// </summary>
        public string UseScene { get; private set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; private set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string? Big { get; private set; }
        /// <summary>
        /// x坐标
        /// </summary>
        public string Xstation { get; private set; }
        /// <summary>
        /// y坐标
        /// </summary>
        public string Ystation { get; private set; }
        /// <summary>
        /// 角度
        /// </summary>
        public string Angle { get; private set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string? Style { get; private set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; private set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string Font { get; private set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public string FontSize {  get; private set; }
        /// <summary>
        /// 透明度
        /// </summary>
        public int? Pellucidity { get; private set; }
        /// <summary>
        /// 高度
        /// </summary>
        public string Hight { get; private set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string Width { get; private set; }
        /// <summary>
        /// 是否合适大小
        /// </summary>
        public string? IsSuitable { get; private set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string? Picture { get; private set; }
        /// <summary>
        /// PicX
        /// </summary>
        public string? PicX { get; private set; }
        /// <summary>
        /// PicY
        /// </summary>
        public string? PicY { get; private set; }
        /// <summary>
        /// PicY
        /// </summary>
        public string GapX { get; private set; }
        /// <summary>
        /// 左右间距
        /// </summary>
        public string GapY { get; private set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; private set; }
        /// <summary>
        /// 使用机构
        /// </summary>
        public string OrgCode { get; private set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; private set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string InputCode { get; private set; }
    }
}