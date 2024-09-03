using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Application.MedicalRecord.Dto.PatientDetails;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.PatientDetails
{
    public record FindPatientDetailsQry : IRequest<PatientDetailsDto>
    {
        /// <summary>
        /// 档案号
        /// </summary>
        [Required]
        public string ArchiveId { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string DeptId { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get; set; }
    }
}