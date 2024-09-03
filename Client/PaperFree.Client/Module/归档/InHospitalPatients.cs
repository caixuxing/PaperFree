using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using PaperFree.Client.Dto;
using PaperFree.Client.Dto.Response.SystemBasicInfo;
using PaperFree.Client.Global;
using PaperFree.Client.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PaperFree.Client.Module.归档
{
    /// <summary>
    /// 在院患者
    /// </summary>
    public partial class InHospitalPatients :   DevExpress.XtraEditors.XtraForm
    {
        private readonly IHttpClientFactory httpClientFactory;
        public InHospitalPatients(IHttpClientFactory httpClientFactory)
        {
            InitializeComponent();
            this.httpClientFactory = httpClientFactory;
            this.Load += InHospitalPatients_Load;
        }

        private async void InHospitalPatients_Load(object sender, EventArgs e)
        {
            using (IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this, Loading.options))
            {

                await LoadBind();
                SplashScreenManager.CloseOverlayForm(handle);
            }
        }


        async Task LoadBind()
        {
            await this.dict_Orgt();
            await this.dict_dept();
            await this.GridDate_load();
        }
        /// <summary>
        /// 初始化加载机构
        /// </summary>
        /// <returns></returns>
        private async Task dict_Orgt() {
            var result = await new HttpTools(httpClientFactory).GetAsync<ResultJson<List<SystemDictHospital>>>($"{Api.org}?{new {pageIndex=1 }.ToUrlParams()}");
            if (result.HttpStatus == System.Net.HttpStatusCode.OK)
            {
                lookUpEdit_org.Properties.DataSource = result.Data;
                lookUpEdit_org.Properties.DisplayMember= "orG_NAME";
                lookUpEdit_org.Properties.ValueMember = "orG_CODE";
                lookUpEdit_org.EditValue = result.Data.FirstOrDefault()?.orG_CODE;

            }
            
        }
        /// <summary>
        /// 初始化加载科室
        /// </summary>
        /// <returns></returns>
        private async Task dict_dept() {
            var result = await new HttpTools(httpClientFactory).GetAsync<ResultJson<List<SystemDept >>>($"{Api.dept}?PageSize=10&PageIndex=1");
            if (result.HttpStatus==System.Net.HttpStatusCode.OK)
            {
                List<SystemDept> dept_ = result.Data.Where(x=>x.enabled=="Y").ToList();
                dept_.Insert(0,new SystemDept { depT_ID = "ALL", depT_NAME = "所有科室" });
                lookUpEdit_dept.Properties.DataSource = dept_;
                lookUpEdit_dept.Properties.DisplayMember = "depT_NAME";
                lookUpEdit_dept.Properties.ValueMember = "depT_ID";
                lookUpEdit_dept.EditValue = dept_.FirstOrDefault()?.depT_ID;
            }
        }
        /// <summary>
        /// 初始化加载列表数据 默认加载10天
        /// </summary>
        /// <returns></returns>
        private async Task GridDate_load() {

            Dtoparam para=new Dtoparam ();
            para.pagesize = 100;
            para.keyWord = "Test";
            var result = await new HttpTools(httpClientFactory).GetAsync<ResultJson<dynamic>>($"{Api.MedicalRecordControllers}?{para.ToUrlParams()}");
            if (result.HttpStatus==System.Net.HttpStatusCode.OK)
            {
                
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }
    }

    public class Dtoparam
    {
        public int pageindex { get; set; }

        public int pagesize { get; set; }

        public string keyWord  { get; set; }
    }
}
