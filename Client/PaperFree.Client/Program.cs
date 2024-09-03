using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using Microsoft.Extensions.DependencyInjection;
using PaperFree.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaperFree.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            using (Mutex mutex = new Mutex(true, Application.ProductName, out bool isFirstOpen))
            {
                //皮肤全局注册
                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();
                UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");
                if (isFirstOpen)
                {
                    //异常捕获处理
                    BindExceptionHandler();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //注册IHttpClientFactory
                    var services = new ServiceCollection();
                    services.AddHttpClient();
                    var serviceProvider = services.BuildServiceProvider();
                    var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                    //显示登录页
                    Login login = new Login(httpClientFactory);
                    login.ShowDialog();
                    if (login.DialogResult == DialogResult.OK)
                    {
                        //启动系统Main窗体
                        Application.Run(new Main(httpClientFactory));
                    }
                    else return;
                }
                else
                {
                    XtraMessageBox.Show("应用程序已经在运行中...", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Environment.Exit(1);

                }
            }
           
        }

        /// <summary>
        /// 绑定程序中的异常处理
        /// </summary>
        private static void BindExceptionHandler()
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理未捕获的异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        /// <summary>
        /// 处理UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            if (ex.GetType() == typeof(CustomException))
            {
                XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ex.GetType() == typeof(TaskCanceledException))
            {
                XtraMessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string str = GetExceptionMsg(e.Exception, e.ToString());
            XtraMessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 处理未捕获的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            XtraMessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        /// <summary>h
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        private static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now);
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                sb.AppendLine("【异常方法】：" + ex.TargetSite);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
