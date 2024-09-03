using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 人员与科室表
    /// </summary>
    [Table("BASE_ORG_DEPT_MEMBER")]
    public class BaseOrgDeptMember
    {
        public string ID { get; set; }

        public string ORG_CODE { get; set; }

        public string HOSP_CODE { get; set; }

        public string DEPT_ID { get; set; }

        public string MEMBER_ID { get; set; }

        public string MASTER_FLAG { get; set; }

        public string CREATE_USER { get; set; }

        public string? UPDATE_USER { get; set; }

        public DateTime? CREATE_DATE { get; set; }
        public DateTime? UPDATE_DATE { get; set; }

        public string IS_DELETE { get; set; }
    }
}