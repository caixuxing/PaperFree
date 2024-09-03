using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo
{
    /// <summary>
    /// 出院病人分页列表
    /// </summary>
    public record OutpatientInfoDto
    {
        /// <summary>
        /// 档案号
        /// </summary>
        public string ArchiveId {  get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public WorkFlowState Status { get; set; }
        public string StatusName { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string AdmissId { get; set; }
        /// <summary>
        /// 住院次数
        /// </summary>
        public int VisitId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum SexType { get; set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string SexName { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime EnterDate { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public DateTime OutDate { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string OutDeptCode { get; set; }
        /// <summary>
        /// 主治医生编号
        /// </summary>
        public string? DoctorZzysCode { get; set; }
        /// <summary>
        /// 住院医生编号
        /// </summary>
        public string? DoctorZyysCode { get; set; }
        /// <summary>
        /// 责任护士编号
        /// </summary>
        public string? ChargeNurseCode { get; set; }
        /// <summary>
        /// 住院 天数
        /// </summary>
        public int Days {  get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode {  get; set; }
        /// <summary>
        /// 出院科室名称
        /// </summary>
        public string OutDeptName {  get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode {  get; set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string InputCode {  get; set; }

    }
}