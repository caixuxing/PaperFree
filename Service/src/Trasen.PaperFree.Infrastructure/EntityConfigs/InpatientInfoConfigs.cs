using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 在院病人就诊信息表
    /// </summary>
    public class InpatientInfoConfigs : IEntityTypeConfiguration<InpatientInfo>
    {
        public void Configure(EntityTypeBuilder<InpatientInfo> builder)
        {
            builder.ToTable("INPATIENT_INFO").HasComment("在院病人就诊信息表");
            builder.HasKey(t => t.ArchiveId);
            builder.Property(x => x.ArchiveId).HasColumnName("ARCHIVE_ID").HasMaxLength(50).HasComment("档案号（主键）");
            builder.Property(x => x.HospRecordId).HasColumnName("HOSP_RECORD_ID").HasMaxLength(50).HasComment("档案号（主键）");
            builder.Property(x => x.AdmissId).HasColumnName("ADMISS_ID").HasMaxLength(50).HasComment("住院号");
            builder.Property(x => x.InpatientId).HasColumnName("INPATIENT_ID").HasMaxLength(50).HasComment("住院ID");
            builder.Property(x => x.VisitId).HasColumnName("VISIT_ID").HasComment("住院次数");
            builder.Property(x => x.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("机构编码");
            builder.Property(x => x.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50).HasComment("院区编码");
            builder.Property(x => x.InputCode).HasColumnName("INPUT_CODE").HasMaxLength(50).HasComment("辖区编码");
            builder.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(10).HasComment("姓名");
            builder.Property(x => x.SexType).HasColumnName("SEX_TYPE")
                   .HasConversion(x => x.ToString(), x => (SexEnum)Enum.Parse(typeof(SexEnum), x))
                .HasMaxLength(10).HasComment("性别");
            builder.Property(x => x.DateOfBirth).HasColumnName("DATE_OF_BIRTH").HasComment("出生日期");
            builder.Property(x => x.Age).HasColumnName("AGE").HasMaxLength(3).HasComment("年龄");
            builder.Property(x => x.IdCard).HasColumnName("ID_CARD").HasMaxLength(18).HasComment("身份证号");
            builder.Property(x => x.EnterDate).HasColumnName("ENTER_DATE").HasComment("入院时间");
            builder.Property(x => x.EnterDept).HasColumnName("ENTER_DEPT").HasMaxLength(100).HasComment("入院科室");
            builder.Property(x => x.EnterWardCode).HasColumnName("ENTER_WARD_CODE").HasMaxLength(100).HasComment("病区编码");
            builder.Property(x => x.DoctorZyysCode).HasColumnName("DOCTOR_ZYYS_CODE").HasMaxLength(20).HasComment("住院医生编号");
            builder.Property(x => x.DoctorZzysCode).HasColumnName("DOCTOR_ZZYS_CODE").HasMaxLength(20).HasComment("主治医生编号");
            builder.Property(x => x.DoctorKzrCode).HasColumnName("DOCTOR_KZR_CODE").HasMaxLength(20).HasComment("科主任编号");
            builder.Property(x => x.ChargeNurseCode).HasColumnName("CHARGE_NURSE_CODE").HasMaxLength(20).HasComment("责任护士编号");
        }
    }
}