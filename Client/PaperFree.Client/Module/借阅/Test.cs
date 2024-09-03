using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using PaperFree.Client.Dto.Base;
using PaperFree.Client.Dto.Enums;
using PaperFree.Client.Global;
using PaperFree.Client.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaperFree.Client.Module.借阅
{
    public partial class Test : DevExpress.XtraEditors.XtraForm
    {
        public Test()
        {
            InitializeComponent();
            InitPopMenu();
            this.Load += Test_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Test_Load(object sender, EventArgs e)
        {
            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this, Loading.options);
            await Task.Delay(3000);
            await LoadDataBind();
            SplashScreenManager.CloseOverlayForm(handle);
            handle.Dispose();
        }

        private void InitPopMenu()
        {
            BarButtonItem barItemCurrent = new BarButtonItem
            {
                Name = "barItemCurrent",
                Caption = "编辑"
            };
            barItemCurrent.ItemClick += BarItemCurrent_ItemClick;

            BarButtonItem barItemOther = new BarButtonItem
            {
                Name = "barItemOther",
                Caption = "审核"
            };
            barItemOther.ItemClick += BarItemOther_ItemClick; ;
            BarButtonItem barItemAll = new BarButtonItem
            {
                Name = "barItemAll",
                Caption = "质检"
            };
            popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barItemCurrent), new LinkPersistInfo(barItemOther), new LinkPersistInfo(barItemAll) });
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BarItemOther_ItemClick(object sender, ItemClickEventArgs e)
        {
            var currRowData = gridView1.GetFocusedRow() as DemoCase;
            currRowData.OperateType = Dto.Enums.OperateType.Audit;
            currRowData.FormTitle = $"{OperateType.Audit.GetDescription()}【{currRowData.Name}】";
            ShowTestChiItem(currRowData);
        }

        private async Task LoadDataBind()
        {
            List<DemoCase> dataList = new List<DemoCase>() {
                new DemoCase() { status=StatusType.签约,BeInHospitalNo="333333333",BeInHospitalNumber=1,Name="王老五", Gender=GenderType.男},
                new DemoCase() { status=StatusType.签约,BeInHospitalNo="22222222",BeInHospitalNumber=3,Name="赵奇", Gender=GenderType.保密},
                new DemoCase() { status=StatusType.签约,BeInHospitalNo="111111",BeInHospitalNumber=6,Name="的可爱", Gender=GenderType.保密},
                new DemoCase() { status=StatusType.签约,BeInHospitalNo="44444444",BeInHospitalNumber=3,Name="达尔王国", Gender=GenderType.保密},
                new DemoCase() { status=StatusType.签约,BeInHospitalNo="555555",BeInHospitalNumber=5,Name="费瓦", Gender=GenderType.保密},
                new DemoCase() { status=StatusType.签约,BeInHospitalNo="66666666",BeInHospitalNumber=8,Name="去维持", Gender=GenderType.保密},
                new DemoCase() { status=StatusType.签约,BeInHospitalNo="77777",BeInHospitalNumber=1,Name="简单沟通", Gender=GenderType.保密},
                new DemoCase() { status=StatusType.待提交,BeInHospitalNo="8888888",BeInHospitalNumber=1,Name="荣威", Gender=GenderType.女},
            };
            await Task.Delay(1000);
            //禁止编辑
            gridView1.OptionsBehavior.Editable = false;
            //true 时，自定义的奇偶行颜色才有效
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.OptionsView.EnableAppearanceOddRow = true;

            /* 开启自定义颜色
             * gridView1.Appearance.EvenRow.BackColor = Color.LightGreen;//偶数行
               gridView1.Appearance.OddRow.BackColor = Color.DarkBlue;//奇数行*/
            //选中行背景色
            gridView1.Appearance.FocusedRow.BackColor = Color.White;
            //取消选中待黑线边框问题
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

            grid_Data.DataSource = dataList;
            gridView1.ClearSelection();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 行内双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (e.GetType() != typeof(DevExpress.Utils.DXMouseEventArgs) || ((DevExpress.Utils.DXMouseEventArgs)e).Button != MouseButtons.Left)
            {
                return;
            }

            int[] pRows = this.gridView1.GetSelectedRows();//传递实体类过去 获取选中的行
            if (pRows.GetLength(0) > 0)
            {
                //获取指定列数据
                string BeInHospitalNo = gridView1.GetRowCellValue(pRows[0], nameof(DemoCase.BeInHospitalNo)).ToString();

                //获取整行数据
                var currRowData = gridView1.GetFocusedRow() as DemoCase;

                XtraMessageBox.Show(JsonConvert.SerializeObject(currRowData), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grid_Data_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.GetType() != typeof(DevExpress.Utils.DXMouseEventArgs) || ((DevExpress.Utils.DXMouseEventArgs)e).Button != MouseButtons.Right)
            {
                return;
            }
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (hInfo.InRow)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void BarItemCurrent_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] pRows = this.gridView1.GetSelectedRows();//传递实体类过去 获取选中的行
            if (pRows.GetLength(0) > 0)
            {
                //获取指定列数据
                string BeInHospitalNo = gridView1.GetRowCellValue(pRows[0], nameof(DemoCase.BeInHospitalNo)).ToString();

                var currRowData = gridView1.GetFocusedRow() as DemoCase;
                currRowData.OperateType = Dto.Enums.OperateType.Edit;
                currRowData.FormTitle = $"{OperateType.Edit.GetDescription()}【{currRowData.Name}】";
                ShowTestChiItem(currRowData);
            }
        }

        public class DemoCase : OperateBaseDto
        {
            public StatusType status { get; set; }

            public string BeInHospitalNo { get; set; }

            public int BeInHospitalNumber { get; set; }

            public string Name { get; set; }

            public GenderType Gender { get; set; }

            public DemoCase()
            {
            }

            public DemoCase(string formTitle, OperateType operateType)
            {
                this.FormTitle = formTitle;
                this.OperateType = operateType;
            }
        }

        public enum StatusType
        {
            签约,

            待提交
        }

        public enum GenderType
        {
            男,

            女,

            保密
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            var currRowData = new DemoCase(OperateType.Add.GetDescription(), OperateType.Add);
            ShowTestChiItem(currRowData);
        }

        private void ShowTestChiItem(DemoCase formData)
        {
            using (TestChiItem frm = new TestChiItem())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowInTaskbar = false;
                frm.Tag = formData;
                frm.ShowDialog();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }
    }
}