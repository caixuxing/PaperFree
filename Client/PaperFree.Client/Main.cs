using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Microsoft.AspNetCore.SignalR.Client;
using PaperFree.Client.Dto;
using PaperFree.Client.Dto.Response.SystemBasicInfo;
using PaperFree.Client.Global;
using PaperFree.Client.Module.借阅;
using PaperFree.Client.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaperFree.Client
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HubConnection _connection;
        public Main(IHttpClientFactory httpClient)
        {
            InitializeComponent();
            _connection = new HubConnectionBuilder()
             .WithUrl($"http://localhost:44389/hub", options => {
                 // options.AccessTokenProvider = async () => await Task.FromResult(Cache.Instance.Get("token")); },
                 options.Headers["Authorization"] = Cache.Instance.Get("token");
             })
             .Build();
            SetFormProperties();
            InitPopMenu();
            httpClientFactory = httpClient;
            this.Load += Main_Load;
        }

        /// <summary>
        /// 窗体初始化加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Main_Load(object sender, EventArgs e)
        {
            using (IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this, Loading.options))
            {
                await SignalRShow();
                await this.InitDataBind();
                this.InitEventRegister();
                SplashScreenManager.CloseOverlayForm(handle);
            }
        }

        /// <summary>
        /// 输出SignalR消息
        /// </summary>
        /// <returns></returns>
        private async Task SignalRShow()
        {
            _connection.On<string>("SendMessage", (str) =>
            {
                this.Invoke(new Action(() => {
                    XtraMessageBox.Show(str, "后台任务", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            });
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 初始化事件注册
        /// </summary>
        private void InitEventRegister()
        {
            //菜单开展、收缩
            this.nav_Menu.MouseDown += Nav_Menu_MouseDown;
            //工作区页签关闭（当前页、其它页、全部）
            this.Tab_workspace.MouseUp += Tab_workspace_MouseUp;

            //退出系统
            this.btn_Exit.Click += Btn_Exit_Click; ;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();

        }


        /// <summary>
        /// 工作区页签关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_workspace_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs EArg = (ClosePageButtonEventArgs)e;
            string name = EArg.Page.Text;//得到关闭的选项卡的text
            var page = Tab_workspace.TabPages.FirstOrDefault(_ => _.Text.Equals(name));
            if (page != null)
            {
                Tab_workspace.TabPages.Remove(page);
                page.Dispose();
                return;
            }
        }

        /// <summary>
        /// 工作区页签关闭（当前页、其它页、全部）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_workspace_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                XtraTabControl tabCtrl = sender as XtraTabControl;
                if (tabCtrl == null) return;
                XtraTabHitInfo info = tabCtrl.CalcHitInfo(e.Location);
                if (info.HitTest == XtraTabHitTest.PageHeader)
                {
                    popupMenu1.ShowPopup(MousePosition);
                }
            }
        }

        /// <summary>
        /// 菜单点击分组（开展、收缩）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nav_Menu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DevExpress.XtraNavBar.NavBarControl navBar = sender as DevExpress.XtraNavBar.NavBarControl;
                DevExpress.XtraNavBar.NavBarHitInfo hitInfo = navBar.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.InGroupCaption && !hitInfo.InGroupButton)
                    hitInfo.Group.Expanded = !hitInfo.Group.Expanded;
            }
        }

        private void InitPopMenu()
        {
            BarButtonItem barItemCurrent = new BarButtonItem
            {
                Name = "barItemCurrent",
                Caption = "关闭当前"
            };
            barItemCurrent.ItemClick += BarItemCurrent_ItemClick;
            BarButtonItem barItemOther = new BarButtonItem
            {
                Name = "barItemOther",
                Caption = "关闭其他"
            };
            barItemOther.ItemClick += BarItemOther_ItemClick;
            BarButtonItem barItemAll = new BarButtonItem
            {
                Name = "barItemAll",
                Caption = "关闭全部"
            };
            barItemAll.ItemClick += BarItemAll_ItemClick;
            popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barItemCurrent), new LinkPersistInfo(barItemOther), new LinkPersistInfo(barItemAll) });
        }

        /// <summary>
        /// 关闭所有页签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarItemAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = Tab_workspace.TabPages.Count - 1; i > 0; i--)
            {
                var tabPage = Tab_workspace.TabPages[i];
                Tab_workspace.TabPages.Remove(tabPage);
                tabPage.Dispose();
            }
        }

        /// <summary>
        /// 关闭其他页签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarItemOther_ItemClick(object sender, ItemClickEventArgs e)
        {
            int index = Tab_workspace.SelectedTabPageIndex;//得到关闭的选项卡的索引
            for (int i = Tab_workspace.TabPages.Count - 1; i > 0; i--)
            {
                if (i != index)
                {
                   var tabPage = Tab_workspace.TabPages[i];
                    Tab_workspace.TabPages.Remove(tabPage);
                    tabPage.Dispose();
                }
            }

        }

        /// <summary>
        /// 关闭当前页签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarItemCurrent_ItemClick(object sender, ItemClickEventArgs e)
        {
            string name = Tab_workspace.SelectedTabPage.Text;//得到关闭的选项卡的text
            var page = Tab_workspace.TabPages.FirstOrDefault(_ => _.Text == name);
            if (page != null)
            {
                Tab_workspace.TabPages.Remove(page);
                page.Dispose();
                return;
            }
        }

            /// <summary>
            /// 初始数据绑定
            /// </summary>
        private async Task InitDataBind()
        {
            //绑定菜单数据
            await CreateMenus();
            //页标签初始化加载工作台
            WorkbenchHomeForm();
        }

        /// <summary>
        /// 工作台首页标签页
        /// </summary>
        private void WorkbenchHomeForm()
        {
            WorkbenchForm frm = new WorkbenchForm(httpClientFactory, _connection);
            XtraTabPage page = new XtraTabPage{ Text="工作台"};
            //设置窗体没有边框 加入到选项卡中  
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.TopLevel = false;
            // 将新生成的Form添加到选择的TabPage里面去
            frm.Parent = page; 
            frm.ControlBox = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            Tab_workspace.TabPages.Insert(0,page);
            //首页不显示关闭按钮
            Tab_workspace.TabPages[0].ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menus">菜单列表</param>
        /// <param name="projectCode">子系统编号</param>
        private async Task CreateMenus()
        {
            //清除菜单项
           // nav_Menu.Groups.Clear();
            var data = await new HttpTools(httpClientFactory).GetAsync<ResultJson<List<SystemMenuDto>>>(Api.menu);
            data.Data.Where(_=>_.SUPER_CODE.Equals("#")).ToList().ForEach(item =>
            {
                NavBarGroup navBarGroup = new NavBarGroup();
                navBarGroup.Caption = item.MENU_NAME;
                navBarGroup.Appearance.Options.UseFont = true;
                navBarGroup.Appearance.ForeColor = ColorTranslator.FromHtml("#666666");
                navBarGroup.Appearance.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
                navBarGroup.AppearanceHotTracked.Options.UseForeColor = true;
                navBarGroup.AppearanceHotTracked.Options.UseFont = true;
                navBarGroup.AppearanceHotTracked.ForeColor = ColorTranslator.FromHtml("#1161E2");
                navBarGroup.AppearanceHotTracked.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
                var childList = data.Data.Where(_ => _.SUPER_CODE.Equals(item.MENU_CODE)).ToList();
                //筛选子级菜单
                navBarGroup = SetMenuItems(navBarGroup, childList);
                this.nav_Menu.Groups.Add(navBarGroup);
            });
        }

        private NavBarGroup SetMenuItems(NavBarGroup navBarGroup, List<SystemMenuDto> menus)
        {
            menus.ForEach(item =>
            {
                NavBarItem navBarItem = new NavBarItem();
                navBarItem.Caption = item.MENU_NAME;
                navBarItem.Name = item.MENU_NAME;
                navBarItem.Tag = item;
                navBarItem.Appearance.Options.UseFont = true;
               // navBarItem.Appearance.ForeColor = ColorTranslator.FromHtml("#DC143C");
                navBarItem.Appearance.Font = new Font("微软雅黑", 9f, FontStyle.Bold);
                navBarItem.AppearanceHotTracked.Options.UseForeColor = true;
                navBarItem.AppearanceHotTracked.Options.UseFont = true;
                //navBarItem.AppearanceHotTracked.ForeColor = ColorTranslator.FromHtml("#1161E2");
                navBarItem.AppearanceHotTracked.Font = new Font("微软雅黑", 9f, FontStyle.Bold);
                navBarGroup.ItemLinks.Add(new NavBarItemLink(navBarItem));
                navBarItem.LinkClicked += NavBarItem_LinkClicked;
            });
            return navBarGroup;
        }

        /// <summary>
        /// 菜单列表单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarItem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            var navbar = sender as NavBarItem;
            //添加选项卡
            var item = navbar.Tag as SystemMenuDto;
            object[] parameters = new object[1];
            parameters[0] = httpClientFactory;
            Assembly assem = Assembly.Load(this.GetType().Namespace.ToString());
            XtraForm frm = (XtraForm)assem.CreateInstance($"{item.NAME_SPACE}.{item.URL_ADDRESS}", true, BindingFlags.Default, null, parameters, null, null);
            //Test frm = new Test();
            AddControlToPage(item.MENU_NAME, frm);
        }





        /// <summary>
        /// 将控件添加到标签页中
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="ctrl"></param>
        private void AddControlToPage(string PageName, Form ctrl)
        {

            bool isExists = false;
            XtraTabPage tempPage = null;
            for (int i = 0; i < this.Tab_workspace.TabPages.Count; i++)
            {
                if (this.Tab_workspace.TabPages[i].Name == PageName)
                {
                    tempPage = this.Tab_workspace.TabPages[i];
                    isExists = true;
                    break;
                }
            }

            if (!isExists)
            {
                XtraTabPage page = new XtraTabPage();
                page.Name = PageName;
                page.Text = PageName;
                //设置窗体没有边框 加入到选项卡中  
                ctrl.FormBorderStyle = FormBorderStyle.None;
                ctrl.TopLevel = false;
                ctrl.Parent = page;  // 将新生成的Form添加到选择的TabPage里面去
                ctrl.ControlBox = false;
                ctrl.Dock = DockStyle.Fill;
                ctrl.Show();
                this.Tab_workspace.TabPages.Add(page);
                this.Tab_workspace.SelectedTabPage = page;
                //工作区页签关闭事件
                this.Tab_workspace.CloseButtonClick += Tab_workspace_CloseButtonClick;
            }
            else
            {
                this.Tab_workspace.SelectedTabPage = tempPage;
            }

        }

        /// <summary>
        /// 设置窗体属性
        /// </summary>
        protected void SetFormProperties()
        {
            //客户窗体加载默认大小
           this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //默认最小大小
            this.MinimumSize = new System.Drawing.Size(870, 600);
            //工作区
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
           // 最大化
             this.WindowState = FormWindowState.Maximized;
            //禁止窗体拉伸
           this.FormBorderStyle = FormBorderStyle.None;
        }
    }
}