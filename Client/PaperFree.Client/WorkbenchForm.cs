using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using PaperFree.Client.Module.借阅;
using PaperFree.Client.Utils;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Data.Helpers.FindSearchRichParser;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace PaperFree.Client
{
    public partial class WorkbenchForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly IHttpClientFactory httpClientFactory;

        private readonly  HubConnection _connection;

        public WorkbenchForm(IHttpClientFactory httpClientFactory,HubConnection hubConnection)
         {
            InitializeComponent();
            this.httpClientFactory = httpClientFactory;
            _connection = hubConnection;
            this.Load += WorkbenchForm_Load;
        }

        private  void WorkbenchForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test chiId = new Test();
            chiId.StartPosition = FormStartPosition.CenterScreen;
            chiId.ShowInTaskbar = false;
            chiId.ShowDialog();
        }

        /// <summary>
        /// SignalR 发送信息给客户端( 一对一模式)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TestSignalR_Click(object sender, EventArgs e)
        {
            _connection.InvokeAsync("SendUser",textEdit1.EditValue.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _connection.InvokeAsync("Send", textEdit1.EditValue.ToString());
        }
    }
}