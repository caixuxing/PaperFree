using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo
{
    internal class InpatientInfoDto
    {
        public string Status {  get; set; }
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
        public string  SexName { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime EnterDate { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        public string EnterDept { get; set; }
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
        /// 住院天数
        /// </summary>
        public int days {  get; set; }
    }
}
