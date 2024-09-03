using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    public class AnNotAtionTableConfigs : IEntityTypeConfiguration<AnNotAtionTable>
    {
        public void Configure(EntityTypeBuilder<AnNotAtionTable> builder)
        {
            builder.ToTable("ANNOTATION_TABLE").HasComment("批注表");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");
            builder.Property(t => t.Archiveid).HasColumnName("ARCHIVE_ID").HasMaxLength(50).HasComment("档案号");
            builder.Property(t => t.FileId).HasColumnName("FILE_ID").HasMaxLength(50).HasComment("文件id");
            builder.Property(t => t.AnNotAtIOn).HasColumnName("ANNOTATION").HasMaxLength(1000).HasComment("批注内容");
            builder.Property(t => t.Deptcode).HasColumnName("DEPT_CODE").HasMaxLength(100).HasComment("批注科室编码");
            builder.Property(t => t.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("机构编码");
            builder.Property(t => t.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50).HasComment("院区编码");
            builder.Property(t => t.Remark).HasColumnName("REMARK").HasMaxLength(200).HasComment("备注");
            builder.Property(t => t.Lower).HasColumnName("LOWER").HasMaxLength(20).HasComment("评分 ");
            builder.Property(t => t.AnNotAtIOnDate).HasColumnName("AN_NOT_ATION_DATE").HasComment("批注日期");
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