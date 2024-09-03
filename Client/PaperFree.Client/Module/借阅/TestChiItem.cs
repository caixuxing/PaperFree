using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using PaperFree.Client.Dto.Enums;
using PaperFree.Client.Global;
using PaperFree.Client.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PaperFree.Client.Module.借阅.Test;

namespace PaperFree.Client.Module.借阅
{
    public partial class TestChiItem : DevExpress.XtraEditors.XtraForm
    {
        private OperateType? operateType;

        public TestChiItem()
        {
            InitializeComponent();
            this.Load += TestChiItem_Load;
        }

        private async void TestChiItem_Load(object sender, EventArgs e)
        {
            using (IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this, Loading.options))
            {
                var item = this.Tag as DemoCase;
                this.Text = item.FormTitle;
                operateType = item.OperateType;
                await Task.Delay(5000);
                if (item.OperateType.Equals(OperateType.Edit))
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            textEdit1.Text = item.BeInHospitalNo;
                        }));
                    }
                }
                if (item.OperateType.Equals(OperateType.Audit))
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            simpleButton1.Text = "审核";
                        }));
                    }
                }
                SplashScreenManager.CloseOverlayForm(handle);
            }
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this, Loading.options);
            try
            {
                if (OperateType.Edit.Equals(operateType))
                    await EditEvent(handle, cancellationToken.Token);

                if (OperateType.Add.Equals(operateType))
                    await AddEvent(handle, cancellationToken.Token);

                if (OperateType.Audit.Equals(operateType))
                    await AuditEvent(handle, cancellationToken.Token);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseOverlayForm(handle);
            }
            finally
            {
                handle.Dispose();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        private async Task AddEvent(IOverlaySplashScreenHandle handle, CancellationToken token = default)
        {
            await Task.Delay(1000, token);
            SplashScreenManager.CloseOverlayForm(handle);
            XtraMessageBox.Show(operateType.GetDescription(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        private async Task AuditEvent(IOverlaySplashScreenHandle handle, CancellationToken token = default)
        {
            await Task.Delay(7000, token);
            SplashScreenManager.CloseOverlayForm(handle);
            XtraMessageBox.Show(operateType.GetDescription(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        private async Task EditEvent(IOverlaySplashScreenHandle handle, CancellationToken token = default)
        {
            await Task.Delay(3000, token);
            SplashScreenManager.CloseOverlayForm(handle);
            XtraMessageBox.Show(operateType.GetDescription(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 模态窗体请使用Dispose来释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
           
            this.Dispose();
        }
    }
}