using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Global
{
    public class Loading
    {
        /// <summary>
        /// 加载动画参数设置
        /// </summary>
       public static OverlayWindowOptions options = new OverlayWindowOptions(opacity: 0.5, fadeIn: true, fadeOut: true, imageSize: new Size(32, 32));
    }
}
