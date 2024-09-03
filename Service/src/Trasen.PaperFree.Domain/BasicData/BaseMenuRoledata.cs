using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 基础数据_角色字典表
    /// </summary>
    [Table("BASE_MENU_ROLEDATA")]
    public class BaseMenuRoledata
    {
        public string ROLE_CODE { get; set; }
        public string ROLE_NAME { get; set; }
        public string ROLE_DESC { get; set; }
        public string ADMIN_LEVEL { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string ORG_CODE { get; set; }
        public string DELETE_FLAG { get; set; }
        public int ROLE_ID { get; set; }
        public string HOSP_CODE { get; set; }
        public string ID { get; set; }
        public string ROLE_TYPE { get; set; }
    }
}