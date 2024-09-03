using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.MedicalRecord.Entity
{
    /// 在院病人就诊信息表
    /// </summary>
    public record InpatientInfo
    {
        public InpatientInfo()
        {
        }

        /// <summary>
        ///档案号 档案号
        /// </summary>
        public string ArchiveId { get; init; }
        /// <summary>
        /// 就诊号 就诊号
        /// </summary>
        public string HospRecordId { get; private set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string AdmissId { get; private set; }
        /// <summary>
        /// 住院id
        /// </summary>
        public string InpatientId { get; private set; }
        /// <summary>
        /// 住院次数
        /// </summary>
        public int VisitId { get; private set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; private set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; private set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string? InputCode { get; private set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum SexType { get; private set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime DateOfBirth { get; private set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; private set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; private set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime EnterDate { get; private set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        public string EnterDept { get; set; }
        /// <summary>
        /// 病区编码
        /// </summary>
        public string EnterWardCode { get; private set; }
        /// <summary>
        /// 住院医生编号
        /// </summary>
        public string? DoctorZyysCode { get; private set; }
        /// <summary>
        /// 主治医生编号
        /// </summary>
        public string? DoctorZzysCode { get; private set; }
        /// <summary>
        /// 科主任编号
        /// </summary>
        public string? DoctorKzrCode { get; private set; }
        /// <summary>
        /// 责任护士编号
        /// </summary>
        public string? ChargeNurseCode { get; private set; }
    }
}