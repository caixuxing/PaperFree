using DevExpress.XtraSplashScreen;
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

namespace PaperFree.Client.Module.归档
{
    /// <summary>
    /// 出院病例提交
    /// </summary>
    public partial class DischargeCaseSubmission :  DevExpress.XtraEditors.XtraForm
    {
        private readonly IHttpClientFactory httpClientFactory;
        public DischargeCaseSubmission(IHttpClientFactory httpClientFactory)
        {
            InitializeComponent();
            this.Load += DischargeCaseSubmission_Load;
        }
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DischargeCaseSubmission_Load(object sender, EventArgs e)
        {

            SplashScreenManager splashscreenmanager = new SplashScreenManager(this, typeof(WaitForm1), true, true);
            splashscreenmanager.ShowWaitForm();
            splashscreenmanager.SetWaitFormCaption("请等待");
            splashscreenmanager.SetWaitFormDescription("窗体数据加载中......");
            await Task.Delay(5000);

            splashscreenmanager.CloseWaitForm();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

    }
}
