using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 基础数据_角色人员关系表
    /// </summary>
    [Table("BASE_MENU_ROLEUSER")]
    public class BaseMenuRoleuser
    {
        public string ROLE_CODE { get; set; }
        public string MEMBER_CODE { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string DELETE_FLAG { get; set; }
        public string ORG_CODE { get; set; }
        public string ID { get; set; }
        public string HOSP_CODE { get; set; }
    }
}