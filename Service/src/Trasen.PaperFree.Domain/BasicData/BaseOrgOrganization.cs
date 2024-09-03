using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 机构信息
    /// </summary>
    [Table("BASE_ORG_ORGANIZATION")]
    public class BaseOrgOrganization
    {
        public string ID { get; set; }
        public string ORG_CODE { get; set; }
        public string ORG_NAME { get; set; }
        public string PARENT_CODE { get; set; }
        public string ORG_TYPE { get; set; }
        public string SHORT_NAME { get; set; }
        public string ORG_LEVEL { get; set; }
        public string PINYINCODE { get; set; }
        public string FIVECODE { get; set; }
        public string ATTRIBUTIONPROVINCE { get; set; }
        public string ATTRIBUTIONCITY { get; set; }
        public string ATTRIBUTIONCOUNTY { get; set; }
        public string ATTRIBUTIONTOWNSHIP { get; set; }
        public string ATTRIBUTIONVILLAGE { get; set; }
        public string ORG_PROPERTY { get; set; }
        public string AUTHORIZATION { get; set; }
        public string LEGALPERSON { get; set; }
        public string CONTACT { get; set; }
        public string ORG_ADDR { get; set; }
        public string DECLARE { get; set; }
        public string AREA_CODE { get; set; }
        public string ATTRIBUTIONGROUP { get; set; }
        public string ORGANIZATIONCODE { get; set; }
        public string VERSIONNUMBER { get; set; }
        public string CREATE_USER { get; set; }
        public string UPDATE_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string IS_DELETE { get; set; }
        public string BASICINIT { get; set; }
        public string ORG_INTRO { get; set; }
        public string ORG_TRAIT { get; set; }
        public string IS_DISABLED { get; set; }
        public DateTime AUTHORIZATION_DATE { get; set; }
        public string DEACTIVATED_REASON { get; set; }
        public string EXPIRATION_REMINDER { get; set; }
        public string SYS_TYPE { get; set; }
        public string EMR_SYNC { get; set; }
        public string OTHER_ORGID { get; set; }
    }
}