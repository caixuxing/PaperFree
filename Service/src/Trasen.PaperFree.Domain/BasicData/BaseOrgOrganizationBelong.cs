using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 机构归属管理
    /// </summary>
    [Table("BASE_ORG_ORGANIZATION_BELONG")]
    public class BaseOrgOrganizationBelong
    {
        public string ID { get; set; }
        public string ORG_CODE { get; set; }
        public string LOWER_ORG_CODE { get; set; }
        public string LOWER_ORG_NAME { get; set; }
        public string LOWER_ORG_LEVEL { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string IS_DELETE { get; set; }
    }
}