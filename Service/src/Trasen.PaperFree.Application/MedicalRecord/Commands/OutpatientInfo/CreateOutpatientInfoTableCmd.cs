using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Application.MedicalRecord.Commands.OutpatientInfo
{
    /// <summary>
    /// 新增患者信息入参
    /// </summary>
    public class CreateOutpatientInfoTableCmd : IRequest<string>
    {

        /// <summary>
        /// 就诊号
        /// </summary>
        public string? HospRecordId { get; set; }
        /// <summary>
        /// 病案号
        /// </summary>
        public string? PatientId { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        [Required]
        public string AdmissId { get; set; }
        /// <summary>
        /// 住院id
        /// </summary>
        public string? InpatientId { get; set; }
        /// <summary>
        /// 住院次数
        /// </summary>
        [Required]
        public int VisitId { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string? InputCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public string SexType { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Required]
        public int Age { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [Required]
        public string IdCard { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        [Required]
        public DateTime EnterDate { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        [Required]
        public DateTime OutDate { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        [Required]
        public string EnterDeptCode { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        [Required]
        public string OutDeptCode { get; set; }
        /// <summary>
        /// 出院病区代码
        /// </summary>
        public string? OutWardCode { get; set; }
        /// 科主任编号
        /// </summary>
        public string? DoctorKzrCode { get; set; }
        /// 主任医生编码
        /// </summary>
        public string? DoctorZrysCode { get; set; }
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
        /// 首页录入标志（编目标志）
        /// </summary>
        public string? BasyStatus { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// 病人入院方式，1.住院  2.门急诊电子病历
        /// </summary>
        [Required]
        public string CaseType { get; set; }
        /// <summary>
        /// 是否锁定（0未锁定，1锁定，2解锁,3解锁中）
        /// </summary>
        public string? IsLock { get; private set; }
        /// <summary>
        /// 值验证
        /// </summary>
        public class CreateOutpatientInfoTableCmdValidate : AbstractValidator<CreateOutpatientInfoTableCmd>
        {
            public CreateOutpatientInfoTableCmdValidate() 
            {
                RuleFor(x => x.AdmissId).NotEmpty().WithMessage("住院号不能为空").MaximumLength(50).WithMessage("住院号长度不能超过50");
                RuleFor(x => x.VisitId).NotEmpty().WithMessage("住院次数不能为空");
                RuleFor(x => x.HospRecordId).MaximumLength(50).WithMessage("就诊号长度不能超过50");
                RuleFor(x => x.PatientId).MaximumLength(18).WithMessage("病案号长度不能超过18");
                RuleFor(x => x.InpatientId).MaximumLength(50).WithMessage("住院ID长度不能超过50");
                RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空").MaximumLength(20).WithMessage("机构编码长度不能大于20");
                RuleFor(x => x.HospCode).MaximumLength(20).WithMessage("院区编码长度不能大于20");
                RuleFor(x => x.InputCode).MaximumLength(20).WithMessage("辖区编码长度不能大于20");
                RuleFor(x => x.Name).NotEmpty().WithMessage("患者姓名不能为空").MaximumLength(20).WithMessage("患者姓名长度不能大于20");
               // RuleFor(x => x.SexType).NotEmpty().WithMessage("患者性别不能为空").MaximumLength(4).WithMessage("患者性别长度不能大于4");
                RuleFor(x => x.IdCard).NotEmpty().WithMessage("身份证号码不能为空").MaximumLength(18).WithMessage("身份证号长度不能大于18");
                RuleFor(x => x.EnterDate).NotEmpty().WithMessage("入院时间不能为空");
                RuleFor(x => x.EnterDeptCode).NotEmpty().WithMessage("入院科室不能为空").MaximumLength(100).WithMessage("入院科室编码长度不能大于100");
                RuleFor(x=>x.OutDeptCode).NotEmpty().WithMessage("出院科室编码不能为空").MaximumLength(100).WithMessage("出院科室编码长度不能大于100");
                RuleFor(x => x.OutWardCode).MaximumLength(100).WithMessage("病区编码长度不能大于100");
                RuleFor(x => x.DoctorKzrCode).MaximumLength(20).WithMessage("科主任编号长度不能大于20");
                RuleFor(x => x.DoctorZrysCode).MaximumLength(20).WithMessage("主任医生编号长度不能大于20");
                RuleFor(x => x.DoctorZyysCode).MaximumLength(20).WithMessage("住院医生编号长度不能大于20");
                RuleFor(x => x.DoctorZzysCode).MaximumLength(20).WithMessage("主治医生编号长度不能大于20");
                RuleFor(x => x.ChargeNurseCode).MaximumLength(20).WithMessage("责任护士编号长度不能大于20");
                RuleFor(x => x.CaseType).NotEmpty().WithMessage("病历分类不能为空").MaximumLength(20).WithMessage("病历分类长度不能大于20");
            }
        
        }
    

    }
}
