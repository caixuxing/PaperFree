using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 科室字典
    /// </summary>
    [Table("BASE_ORG_DEPARTMENT")]
    public class BaseOrgDepartment
    {
        public string ID { get; set; }
        public string ORG_CODE { get; set; }
        public string HOSP_CODE { get; set; }
        public string DEPT_ID { get; set; }
        public string DEPT_NAME { get; set; }
        public string WB_CODE { get; set; }
        public string PY_CODE { get; set; }
        public string ENABLED { get; set; }
        public int ORDER_NUM { get; set; }
        public string PARENT_CODE { get; set; }
        public string ADDRESS { get; set; }
        public string DUTY_PHONE { get; set; }
        public int BED_NUM { get; set; }
        public string TYPE_CODE { get; set; }
        public string DUTY_SCOPE { get; set; }
        public string PLAT_DEPT_CODE { get; set; }
        public string REMARK { get; set; }
        public string CREATE_USER { get; set; }
        public string UPDATE_USER { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string IS_DELETE { get; set; }
        public string D_CODE { get; set; }
        public string LEADER_ID { get; set; }
        public string HEAD_DEPT_ID { get; set; }
        public string HEAD_NURSE_ID { get; set; }
        public string SIMP_DEPT_NAME { get; set; }
        public string SUBJECT_CODE { get; set; }
        public string SUBJECT { get; set; }
        public int SUBJECT_ORDER { get; set; }
        public string INTRODUCE { get; set; }
        public string DEPTAREA { get; set; }
        public string IS_REGISTER { get; set; }
        public string IS_DRUG_APPLY { get; set; }
        public string IS_ITEMS_APPLY { get; set; }
        public string IS_DEVICES_APPLY { get; set; }
        public string DEPARTMENT_FEATURE { get; set; }
        public int EMR_DEPT_ID { get; set; }
    }
}