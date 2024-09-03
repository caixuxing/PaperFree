using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using PaperFree.Client.Dto;
using PaperFree.Client.Dto.Response.BorrowDto;
using PaperFree.Client.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaperFree.Client.Module.借阅
{
    public partial class BorrowList : DevExpress.XtraEditors.XtraForm
    {
        private readonly IHttpClientFactory httpClientFactory;
        public BorrowList(IHttpClientFactory httpClientFactory)
        {
            InitializeComponent();
            x = Width;
            y = Height;
            setTag(this);
            this.httpClientFactory = httpClientFactory;
            this.Load += BorrowList_Load;
        }

        private async void BorrowList_Load(object sender, EventArgs e)
        {

            OverlayWindowOptions options = new OverlayWindowOptions(opacity: 0.5, fadeIn: true, fadeOut: true, imageSize: new Size(64, 64));
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this, options);
            await Task.Delay(5000);
            var client = httpClientFactory.CreateClient();
            var result = await new HttpTools(httpClientFactory).GetAsync<ResultJson<BorrowDto>>("medicalrecord", "c8820e3a-3752-764c-d749-75b04d9cf869");
            if (result.HttpStatus != System.Net.HttpStatusCode.OK)
                throw new Exception("");
            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(() =>
                {

                    labelControl1.Text = result.Data.name;
                }));
            }
            SplashScreenManager.CloseOverlayForm(handle);
            handle.Dispose();
        }
        #region 控件大小随窗体大小等比例缩放

        private readonly float x; //定义当前窗体的宽度
        private readonly float y; //定义当前窗体的高度

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0) setTag(con);
            }
        }

        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    var mytag = con.Tag.ToString().Split(';');
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * newx); //宽度
                    con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * newy); //高度
                    con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * newx); //左边距
                    con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * newy); //顶边距
                    var currentSize = Convert.ToSingle(mytag[4]) * newy; //字体大小                   
                    if (currentSize > 0) con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    con.Focus();
                    if (con.Controls.Count > 0) setControls(newx, newy, con);
                }
        }


        /// <summary>
        /// 重置窗体布局
        /// </summary>
        private void ReWinformLayout()
        {
            var newx = Width / x;
            var newy = Height / y;
            setControls(newx, newy, this);

        }

        #endregion

        private void BorrowList_Resize(object sender, EventArgs e)
        {
                //重置窗口布局
                ReWinformLayout();
        }
    }
}