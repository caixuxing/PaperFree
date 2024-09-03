using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 基础项目_个人参数列表
    /// </summary>

    [Table("BASE_PERSONAL_PARAMETER")]
    public class BasePersonalParameter
    {
        public string ID { get; set; }
        public string ORG_CODE { get; set; }
        public string HOSP_CODE { get; set; }
        public string MEMBER_CODE { get; set; }
        public string CODE { get; set; }
        public string VALUE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string IS_DELETE { get; set; }
        public string PY_CODE { get; set; }
        public string WB_CODE { get; set; }
        public string GROUP_ID { get; set; }
        public string PARAM_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
    }
}