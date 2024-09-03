using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 基础数据_角色菜单关系表
    /// </summary>
    [Table("BASE_MENU_ROLEMENUP")]
    public class BaseMenuRolemenup
    {
        public string ROLE_CODE { get; set; }
        public string MENU_CODE { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string DELETE_FLAG { get; set; }
        public string ID { get; set; }
        public string ORG_CODE { get; set; }
        public string HOSP_CODE { get; set; }
    }
}