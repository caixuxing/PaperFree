using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    ///
    /// </summary>
    [Table("BASE_MENU_USERCOMMON")]
    public class BaseMenuUsercommon
    {
        public string ID { get; set; }
        public string ORG_CODE { get; set; }
        public string HOSP_CODE { get; set; }
        public string USER_CODE { get; set; }
        public string MENU_CODE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public string IS_DELETE { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_URL { get; set; }
    }
}