using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaperFree.Client.Dto;
using PaperFree.Client.Global;
using PaperFree.Client.Utils;
using PaperFree.Client.Utils.DevValidate;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaperFree.Client
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        private readonly IHttpClientFactory httpClientFactory;
        public Login(IHttpClientFactory httpClientFactory)
        {
            InitializeComponent();
            this.InitialCheckInvalid();
            this.httpClientFactory = httpClientFactory;
            this.Load += Login_Load;
        }
        private void Login_Load(object sender, EventArgs e)
        {
            //设置文本内容设置为*
            this.txt_password.Properties.PasswordChar = '*';
            //绑定登录数据
            this.LoginDataBind();
        }

        /// <summary>
        ///
        /// </summary>
        private void LoginDataBind()
        {
            this.txt_username.EditValue = "1101";
            this.txt_password.EditValue = "0618";
            this.txt_orgcode.EditValue = "430321321";
            this.txt_hospCode.EditValue = "430321321_01";
        }



        /// <summary>
        /// 验证是否为空
        /// </summary>
        private void InitialCheckInvalid()
        {
            dxValidationProvider1.ValidationMode = ValidationMode.Manual;
            dxValidationProvider1.Validate();
            /* 
             * 调用正则表达式验证
             * dxValidationProvider1.SetValidationRule(this.txt_username, ControlRule.AddValueRex(CustomRuleByRegex.strPhone, false,"必须为电话号码"));
             dxValidationProvider1.SetIconAlignment(this.txt_username, ErrorIconAlignment.MiddleRight);*/

            dxValidationProvider1.SetValidationRule(this.txt_username, ControlRule.NotEmpty());
            dxValidationProvider1.SetIconAlignment(this.txt_username, ErrorIconAlignment.MiddleRight);

            dxValidationProvider1.SetValidationRule(this.txt_password, ControlRule.NotEmpty());
            dxValidationProvider1.SetIconAlignment(this.txt_password, ErrorIconAlignment.MiddleRight);


        }

        private async void btn_Login_Click(object sender, EventArgs e)
        {
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
            try
            {
                if (!dxValidationProvider1.Validate())
                {
                    SplashScreenManager.CloseOverlayForm(handle);
                    XtraMessageBox.Show("参数校验未通过！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string username = this.txt_username.EditValue.ToString();
                string password = this.txt_password.EditValue.ToString();
                string orgCode = this.txt_orgcode.EditValue.ToString();
                string hospCode = this.txt_hospCode.EditValue.ToString();
                var client = httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(10);
                StringContent strcontent = new StringContent(JsonConvert.SerializeObject(new { loginName = username, loginpassword = password, orgCode = orgCode, hospCode = hospCode }), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{Global.ApplicationProject.domainName}{Api.Login}", strcontent);
                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<ResultJson<string>>(result);
                if (dto.HttpStatus == System.Net.HttpStatusCode.OK)
                {
                    Utils.Cache.Instance.Add("token", dto.Data);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    SplashScreenManager.CloseOverlayForm(handle);
                    XtraMessageBox.Show("登录失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (TaskCanceledException)
            {
                //记录真实错误日志，如：接口地址+请求参数 转换成JSON 字符串存储在日志表中。
                throw new CustomException("登录请求超时");
            }
            finally
            {
                handle.Dispose();
            }
        }

        /// <summary>
        /// userName控件按下监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_username_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter) btn_Login_Click(sender, e);
        }
    }
}