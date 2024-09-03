using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.MedicalRecord.Commands.OutpatientInfo
{
    public record CreateOutpatientEmergencyCmd:IRequest<string>
    {

        /// <summary>
        /// 就诊号
        /// </summary>
        [Required]
        public string HospRecordId { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        [Required]
        public string AdmissId { get; init; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode { get; init; }
        /// <summary>
        /// 院区编码
        /// </summary>
        [Required]
        public string HospCode { get; init; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public SexEnum SexType { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>

        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; set; }
        /// <summary>
        /// 接诊时间
        /// </summary>
        [Required]
        public DateTime SeePatientsDate { get; set; }
        /// <summary>
        /// 接诊科室
        /// </summary>
        [Required]
        public string SeeDeptCode { get; set; }
        /// <summary>
        /// 接诊医生编码
        /// </summary>
        [Required]
        public string ReceiveDoctorCode { get; set; }
        /// <summary>
        /// 门诊诊断编码
        /// </summary>
        [Required]
        public string IcdCode { get; set; }
        /// <summary>
        /// 门诊诊断名称
        /// </summary>
        [Required]
        public string IcdName { get; set; }
        public class CreateOutpatientEmergencyCmdValidate : AbstractValidator<CreateOutpatientEmergencyCmd>
        {
            public CreateOutpatientEmergencyCmdValidate()
            {
                RuleFor(x => x.HospRecordId).NotEmpty().WithMessage("就诊号不能为空").MaximumLength(50).WithMessage("就诊号长度不能超过50");
                RuleFor(x => x.AdmissId).NotEmpty().WithMessage("住院号不能为空").MaximumLength(50).WithMessage("住院号长度不能超过50");
                RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空").MaximumLength(50).WithMessage("机构编码长度不能大于50");
                RuleFor(x => x.HospCode).MaximumLength(50).WithMessage("院区编码长度不能大于50");
                RuleFor(x => x.Name).NotEmpty().WithMessage("患者姓名不能为空").MaximumLength(20).WithMessage("患者姓名长度不能大于20");
                RuleFor(x => x.IdCard).MaximumLength(18).WithMessage("身份证号长度不能大于18");
                RuleFor(x => x.SeeDeptCode).NotEmpty().WithMessage("接诊科室不能为空").MaximumLength(50).WithMessage("接诊科室编码长度不能大于50");
                RuleFor(x => x.SeePatientsDate).NotEmpty().WithMessage("接诊时间不能为空");
                RuleFor(x => x.ReceiveDoctorCode).NotEmpty().WithMessage("接诊医生编码不能为空").MaximumLength(50).WithMessage("接诊医生编码不能大于50");
                RuleFor(x => x.IcdCode).NotEmpty().WithMessage("门诊诊断编码不能为空").MaximumLength(50).WithMessage("门诊诊断编码长度不能大于50");
                RuleFor(x => x.Name).NotEmpty().WithMessage("门诊诊断名称不能为空").MaximumLength(100).WithMessage("门诊诊断名称长度不能大于100");
            }
        }
    }
}
