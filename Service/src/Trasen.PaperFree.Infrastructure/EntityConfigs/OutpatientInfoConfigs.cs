using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 在院病人就诊信息表
    /// </summary>
    public class OutpatientInfoConfigs : IEntityTypeConfiguration<OutpatientInfo>
    {
        public void Configure(EntityTypeBuilder<OutpatientInfo> builder)
        {
            builder.ToTable("OUTPATIENT_INFO").HasComment("在院病人就诊信息表");
            builder.HasKey(t => t.ArchiveId);
            builder.Property(x => x.ArchiveId).HasColumnName("ARCHIVE_ID").HasMaxLength(50).HasComment("档案号（主键）");
            builder.Property(x => x.HospRecordId).HasColumnName("HOSP_RECORD_ID").HasMaxLength(50).HasComment("就诊号");
            builder.Property(x => x.PatientId).HasColumnName("PATIENT_ID").HasMaxLength(50).HasComment("病案号");
            builder.Property(x => x.AdmissId).HasColumnName("ADMISS_ID").HasMaxLength(50).HasComment("住院号");
            builder.Property(x => x.InpatientId).HasColumnName("INPATIENT_ID").HasMaxLength(50).HasComment("住院ID");
            builder.Property(x => x.VisitId).HasColumnName("VISIT_ID").HasComment("住院次数");
            builder.Property(x => x.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("机构编码");
            builder.Property(x => x.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50).HasComment("院区编码");
            builder.Property(x => x.InputCode).HasColumnName("INPUT_CODE").HasMaxLength(50).HasComment("辖区编码");
            builder.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(20).HasComment("姓名");
            builder.Property(x => x.SexType).HasColumnName("SEX_TYPE")
                   .HasConversion(x => x.ToString(), x => (SexEnum)Enum.Parse(typeof(SexEnum), x))
                   .HasMaxLength(10).HasComment("性别");
            builder.Property(x => x.DateOfBirth).HasColumnName("DATE_OF_BIRTH").HasComment("出生日期");
            builder.Property(x => x.Age).HasColumnName("AGE").HasComment("年龄");
            builder.Property(x => x.IdCard).HasColumnName("ID_CARD").HasMaxLength(18).HasComment("身份证号");
            builder.Property(x => x.EnterDate).HasColumnName("ENTER_DATE").HasComment("入院时间");
            builder.Property(x => x.OutDate).HasColumnName("OUT_DATE").HasComment("出院时间");
            builder.Property(x => x.EnterDeptCode).HasColumnName("ENTER_DEPT_CODE").HasMaxLength(100).HasComment("入院科室");
            builder.Property(x => x.OutDeptCode).HasColumnName("OUT_DEPT_CODE").HasMaxLength(100).HasComment("出院科室");
            builder.Property(x => x.OutWardCode).HasColumnName("OUT_WARD_CODE").HasMaxLength(100).HasComment("病区编码");
            builder.Property(x => x.DoctorKzrCode).HasColumnName("DOCTOR_KZR_CODE").HasMaxLength(20).HasComment("科主任编号");
            builder.Property(x => x.DoctorZrysCode).HasColumnName("DOCTOR_ZRYS_CODE").HasMaxLength(20).HasComment("主任医生编号");
            builder.Property(x => x.DoctorZyysCode).HasColumnName("DOCTOR_ZYYS_CODE").HasMaxLength(20).HasComment("住院医生编号");
            builder.Property(x => x.DoctorZzysCode).HasColumnName("DOCTOR_ZZYS_CODE").HasMaxLength(20).HasComment("主治医生编号");
            builder.Property(x => x.ChargeNurseCode).HasColumnName("CHARGE_NURSE_CODE").HasMaxLength(20).HasComment("责任护士编号");
            builder.Property(x => x.BasyStatus).HasColumnName("BASY_STATUS").HasMaxLength(20).HasComment("首页录入标志（编目标志）");
            builder.Property(x => x.Days).HasColumnName("DAYS").HasMaxLength(20).HasComment("年龄(天)");
            builder.Property(x => x.IsOverdate).HasColumnName("IS_OVERDATE").HasMaxLength(20).HasComment("是否逾期");
            builder.Property(x => x.CreateDateTime).HasColumnName("CREATE_DATE_TIME").HasComment("创建时间");
            builder.Property(x => x.Fileflag).HasColumnName("FILE_FLAG").HasMaxLength(20).HasComment("文件采集标志（0未采集，1已采集）");
            builder.Property(x => x.Status).HasColumnName("STATUS").HasMaxLength(20).HasComment("状态");
            builder.Property(x => x.CaseType).HasColumnName("CASE_TYPE").HasMaxLength(20).HasComment("入院方式，1.门诊  2.住院");
            builder.Property(x => x.IsLock).HasColumnName("IS_LOCK").HasMaxLength(20).HasComment("是否锁定（0未锁定，1锁定，2解锁,3解锁中）");
        }
    }
}