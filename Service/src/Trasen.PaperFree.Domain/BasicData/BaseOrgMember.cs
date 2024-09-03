using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 人员字典
    /// </summary>
    [Table("BASE_ORG_MEMBER")]
    public class BaseOrgMember
    {
        public string ID { get; set; }

        public string ORG_CODE { get; set; }

        public string HOSP_CODE { get; set; }

        public string? MENBER_ID { get; set; }

        public string NAME { get; set; }

        public string SEX { get; set; }

        public string? AGE { get; set; }
        public string IDCARD { get; set; }

        public string? PHONE { get; set; }

        public string? NATION_CODE { get; set; }

        public string? NATION_NAME { get; set; }

        public string? MEMBERTYPE_CODE { get; set; }

        public string? MEMBERTYPE_NAME { get; set; }

        public string? TITLE_CODE { get; set; }

        public string? TITLE_NAME { get; set; }
        public string? SPECIAL_FIELD { get; set; }
        public string? DESCRIPTION { get; set; }

        public string? EMAIL { get; set; }
        public string? HOME_PHONE { get; set; }

        public string? HOME_ADDRESS { get; set; }
        public string? IMG_URL { get; set; }

        public string? REMARKS { get; set; }

        public string STATUS { get; set; }

        public string? POSTNATURE_CODE { get; set; }
        public string? POSTNATURE_NAME { get; set; }
        public string MEMBER_ROLE { get; set; }
        public string? DOCTOR_CODE { get; set; }

        public string? DOCTOR_TITLE { get; set; }
        public string PRESCRIBE { get; set; }

        public DateTime? PRESCRIBE_STARTDATE { get; set; }
        public DateTime? PRESCRIBE_ENDDATE { get; set; }

        public string? PRESCRIBEEXPIREDATE { get; set; }

        public string ANESTHESIA { get; set; }
        public string HERBAL { get; set; }
        public string CIPHERPRESCRIPTION { get; set; }

        public string POISONOUS { get; set; }

        public string? NURSE_TITLE { get; set; }

        public string? ISMASTER { get; set; }
        public string PY_CODE { get; set; }

        public string WB_CODE { get; set; }
        public string CREATE_USER { get; set; }
        public string UPDATE_USER { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public DateTime? UPDATE_DATE { get; set; }

        public string IS_DELETE { get; set; }
        public string CODE { get; set; }

        public DateTime? BIRTHDAY { get; set; }

        public string ON_STATE { get; set; }

        public string? RE_EMPLOY { get; set; }

        public string? IS_ONLINE { get; set; }

        public string? DEPT_ID { get; set; }

        public string? OFFICE { get; set; }

        public string? SIGN { get; set; }

        public DateTime? EXPIRY_DATE { get; set; }

        public string? IS_GROUP_LEADER { get; set; }

        public string? FROMHOSP { get; set; }

        public string? IS_REFUND { get; set; }

        public string? PASSWORD { get; set; }

        public string? IS_AUDIT { get; set; }

        public int? SORT_NO { get; set; }

        public string? ANTI_LEVEL { get; set; }

        public string? PSY_RIGHT { get; set; }

        public string? MATER_APPLY { get; set; }

        public string? ZLYP_LEVEL { get; set; }

        public string? UKEY_CODE { get; set; }

        public string? SIGN_URL { get; set; }
        public string? CARDTYPE { get; set; }

        public string? EDUCATION_BACKGROUND { get; set; }

        public string? DEGREE { get; set; }

        public string? GRADUATE_SCHOOL { get; set; }
        public string? NATIVE_PLACE { get; set; }

        public DateTime? ENTRYDATE { get; set; }

        public string? CREDENTIAL_NUMBER { get; set; }

        public string? POLITICS_STATUS { get; set; }

        public string? JUNIOR_SPECIALITY { get; set; }

        public string? SITLEVELID { get; set; }
        public int? EMR_MEMBER_ID { get; set; }

        public string? EMR_ROLE { get; set; }

        public string? IMAGE { get; set; }
        public string? PIC_TYPE { get; set; }
        public string? SPECIAL_COMPETENCIES { get; set; }
    }
}