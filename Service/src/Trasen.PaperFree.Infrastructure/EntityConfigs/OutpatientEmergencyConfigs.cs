using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    public record OutpatientEmergencyConfigs:IEntityTypeConfiguration<OutpatientEmergency>
    {
        public void Configure(EntityTypeBuilder<OutpatientEmergency>builder) {
            builder.ToTable("OUTPATIENT_EMERGENCY").HasComment("归档菜单目录");
            builder.HasKey(p => p.ArchiveId);
            builder.Property(t => t.ArchiveId).HasColumnName("ARCHIVEID").HasMaxLength(50).ValueGeneratedNever()
                    .HasComment("档案号");
            builder.Property(t => t.HospRecordId).HasColumnName("HOSP_RECORD_ID").HasMaxLength(50)
                    .HasComment("就诊号");
            builder.Property(t => t.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50)
                    .HasComment("机构编码");
            builder.Property(t => t.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50)
                    .HasComment("院区编码");
            builder.Property(t => t.Name).HasColumnName("NAME").HasMaxLength(20)
                   .HasComment("患者姓名");
            builder.Property(t => t.SexType).HasColumnName("SEXTYPE").HasMaxLength(50)
                   .HasComment("患者性别");
            builder.Property(t => t.DateOfBirth).HasColumnName("DATE_OF_BIRTH")
                   .HasComment("出生日期");
            builder.Property(t => t.Age).HasColumnName("AGE")
                  .HasComment("患者年龄");
            builder.Property(t => t.IdCard).HasColumnName("ID_CARD").HasMaxLength(50)
                  .HasComment("身份证号码");
            builder.Property(t => t.SeePatientsDate).HasColumnName("SEE_PATIENTS_DATE")
                  .HasComment("接诊时间");
            builder.Property(t => t.SeeDeptCode).HasColumnName("SEEDEPTCODE").HasMaxLength(50)
                  .HasComment("接诊科室");
            builder.Property(t => t.ReceiveDoctorCode).HasColumnName("RECEIVE_DOCTOR_CODE").HasMaxLength(50)
                  .HasComment("接诊医生");
            builder.Property(t => t.IcdCode).HasColumnName("ICD_CODE").HasMaxLength(50)
                  .HasComment("门诊诊断编码");
            builder.Property(t => t.IcdName).HasColumnName("ICD_NAME").HasMaxLength(100)
                  .HasComment("门诊诊断名称");

        }
    }
}
