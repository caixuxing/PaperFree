using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 流程设计
    /// </summary>
    public class ProcessDesignConfigs : IEntityTypeConfiguration<ProcessDesign>
    {
        public void Configure(EntityTypeBuilder<ProcessDesign> builder)
        {
            builder.ToTable("PROCESS_DESIGN").HasComment("流程设计");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever().HasComment("主键ID");
            builder.Property(x => x.ProcessCode).HasColumnName("PROCESS_CODE").HasMaxLength(20).HasComment("流程名称");
            builder.Property(x => x.ProcessName).HasColumnName("PROCESS_NAME").HasMaxLength(20).HasComment("流程代码值");
            builder.Property(x => x.ProcessTempType).HasColumnName("PROCESS_TEMP_TYPE")
                .HasConversion(x => x.ToString(), x => (ProcessTempType)Enum.Parse(typeof(ProcessTempType), x))
                .HasMaxLength(20).HasComment("流程模板类型");
            builder.Property(x => x.DeptCode).HasColumnName("DEPT_CODE").HasMaxLength(100).HasComment("科室编码");
            builder.Property(x => x.IsEnable).HasColumnName("IS_ENABLE").HasComment("是否启用");
            builder.Property(x => x.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("机构编码");
            builder.Property(x => x.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50).HasComment("院区编码");

            builder.Property(a => a.RowVersion).HasColumnName("ROW_VERSION").HasComment("版本号").ValueGeneratedOnAdd().IsRowVersion();
            builder.Property(t => t.CreatorId).HasColumnName("CREATOR_ID").HasMaxLength(50).HasComment("创建人ID");
            builder.Property(t => t.LastModifyId).HasColumnName("LAST_MODIFY_ID").HasMaxLength(50).HasComment("更新者ID");
            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.Property(x => x.IsDeleted).HasColumnName("IS_DELETED").HasComment("删除标识");
            builder.Property(t => t.CreationTime).HasColumnName("CREATION_TIME").HasComment("创建时间");
            builder.Property(t => t.LastModifyTime).HasColumnName("LAST_MODIFY_TIME").HasComment("最后修改时间");
        }
    }
}