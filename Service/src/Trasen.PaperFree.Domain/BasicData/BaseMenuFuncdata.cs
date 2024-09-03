using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 基础数据_菜单字典表
    /// </summary>
    [Table("BASE_MENU_FUNCDATA")]
    public class BaseMenuFuncdata
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        public string SYSTEM_CODE { get; set; }

        /// <summary>
        /// 功能编码
        /// </summary>
        public string MENU_CODE { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string MENU_NAME { get; set; }

        /// <summary>
        /// 上级编码
        /// </summary>
        public string SUPER_CODE { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int SERIAL_NUMBER { get; set; }

        public int MENU_LEVEL { get; set; }
        public string MENU_DESC { get; set; }
        public string DISPLAY_ICO { get; set; }
        public string NAME_SPACE { get; set; }
        public string WINDOWS_NAME { get; set; }
        public string ASSEMBLY_NAME { get; set; }
        public string WINDOWS_STATUS { get; set; }
        public string ICO_NUMBER { get; set; }
        public string WINDOWS_OPENTYPE { get; set; }
        public string TOOLBAR_FLAG { get; set; }
        public string WINDOWS_TYPE { get; set; }
        public string WINDOWS_PARAMETER { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string DELETE_FLAG { get; set; }
        public string URL_ADDRESS { get; set; }
        public string MENU_SHORT { get; set; }
        public string ORG_CODE { get; set; }
        public string HOSP_CODE { get; set; }
        public string ID { get; set; }
        public string MENU_TYPE { get; set; }
        public string DLL_NAME { get; set; }
        public string FORM_NAME { get; set; }
        public string NAMESPACE { get; set; }
        public string CLIENT_TYPE { get; set; }
    }
}