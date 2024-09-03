using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.MedicalRecord.Entity
{
    public record OutpatientInfo
    {
        private OutpatientInfo() { }
   
        /// <summary>
        ///档案号
        /// </summary>
        public string ArchiveId { get; init; }
        /// <summary>
        /// 就诊号
        /// </summary>
        public string HospRecordId { get; init; }
        /// <summary>
        /// 病案号
        /// </summary>
        public string PatientId { get; init; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string AdmissId { get; init; }
        /// <summary>
        /// 住院id
        /// </summary>
        public string InpatientId { get; init; }
        /// <summary>
        /// 住院次数
        /// </summary>
        public int VisitId { get; init; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; init; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; init; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string? InputCode { get; init; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum SexType { get; init; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime DateOfBirth { get; init; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; init; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; init; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime EnterDate { get; init; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public DateTime OutDate { get; init; }
        /// <summary>
        /// 入院科室
        /// </summary>
        public string EnterDeptCode { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string OutDeptCode { get; set; }
        /// <summary>
        /// 出院病区代码
        /// </summary>
        ///         /// <summary>
        public string? OutWardCode { get; set; }
        /// 科主任编号
        /// </summary>
        public string? DoctorKzrCode { get; init; }
        /// 主任医生编码
        /// </summary>
        public string? DoctorZrysCode { get; init; }
        /// <summary>
        /// 主治医生编号
        /// </summary>
        public string? DoctorZzysCode { get; init; }
        /// <summary>
        /// 住院医生编号
        /// </summary>
        public string? DoctorZyysCode { get; init; }
        /// <summary>
        /// 责任护士编号
        /// </summary>
        public string? ChargeNurseCode { get; init; }
        /// <summary>
        /// 首页录入标志（编目标志）
        /// </summary>
        public string? BasyStatus { get; private set; }
        /// <summary>
        /// 年龄(天)
        /// </summary>
        public int? Days { get; init; }
        /// <summary>
        /// 是否逾期
        /// </summary>
        public string? IsOverdate { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDateTime { get; private set; }
        /// <summary>
        ///  文件采集标志（0未采集，1已采集）
        /// </summary>
        public string Fileflag { get; private set; }
        /// <summary>
        /// 状态
        /// </summary>
        public WorkFlowState Status { get; private set; }
        /// <summary>
        /// 病人入院方式，1.门诊  2.住院
        /// </summary>
        public string? CaseType { get; private set; }
        /// <summary>
        /// 是否锁定（0未锁定，1锁定，2解锁,3解锁中）
        /// </summary>
        public string? IsLock { get; private set; }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public OutpatientInfo ChnageStatus(WorkFlowState status)
        {
            Status = status;
            return this;
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="ArchiveId"></param>
        /// <param name=""></param>
        public OutpatientInfo(string archiveid,string hosprecordid,string patientid,string admissid,string inpatientid,int visitid,string orgcode,string hospcode,
            string inputcode,string name,string sextype,DateTime dateofbirth,int age,string idcard,DateTime enterdate ,DateTime outdata,string enterdeptcode
            ,string outdeptcode,string outwardcode,string kzrcode,string zryscode,string zyyscode,string zzyscode,string chargecode,string basystatus,DateTime createdate,
            string casetype,string islock)
        {
            ArchiveId = archiveid;
            HospRecordId = hosprecordid;
            PatientId = patientid;
            AdmissId = admissid;
            InpatientId = inpatientid;
            VisitId = visitid;
            OrgCode = orgcode;
            HospCode = hospcode;
            InputCode= inputcode;
            Name = name;
            DateOfBirth = dateofbirth;
            Age = age;
            IdCard = idcard;
            EnterDate= enterdate;
            OutDate = outdata;
            EnterDeptCode = enterdeptcode;
            OutDeptCode = outdeptcode;
            OutWardCode = outwardcode;
            DoctorKzrCode=kzrcode;
            DoctorZrysCode=zryscode;
            DoctorZyysCode=zyyscode;
            DoctorZzysCode=zzyscode;
            ChargeNurseCode=chargecode;
            BasyStatus= basystatus;
            CreateDateTime = createdate;
            CaseType = casetype;
            IsLock = islock;
            Fileflag = "0";
        }
    }
}