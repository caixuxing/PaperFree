using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo
{
    public record OutpatientEmergencyDto
    {
        /// <summary>
        /// 就诊号
        /// </summary>
        public string HospRecordId {  get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>     
        public string OrgCode { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName {  get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>     
        public string HospCode { get; set; }
        /// <summary>
        /// 院区名称
        /// </summary>
        public string HospName {  get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>     
        public string Name { get; set; }
        /// <summary>
        /// 性别编码
        /// </summary>     
        public SexEnum SexType { get; set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string SexName {  get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 接诊时间
        /// </summary>     
        public DateTime SeePatientsDate { get; set; }
        /// <summary>
        /// 接诊科室编码
        /// </summary>     
        public string SeeDeptCode { get; set; }
        /// <summary>
        /// 接诊科室名称
        /// </summary>
        public string SeeDeptName {  get; set; }
        /// <summary>
        /// 接诊医生编码
        /// </summary>     
        public string ReceiveDoctorCode { get; set; }
        /// <summary>
        /// 接诊医生名称
        /// </summary>
        public string ReceiveDoctorName {  get; set; }
        /// <summary>
        /// 门诊诊断名称
        /// </summary>     
        public string IcdName { get; set; }
    }
}
