using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.RecallRecord.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 召回档案申请关联表
    /// </summary>
    public class RecallApplyAndArchivalConfig : IEntityTypeConfiguration<RecallApplyAndArchival>
    {
        public void Configure(EntityTypeBuilder<RecallApplyAndArchival> builder)
        {
            builder.ToTable("RECALL_APPLY_AND_ARCHIVAL").HasComment("召回档案申请关联表");
            builder.HasIndex(x => x.RecallApplyId).HasDatabaseName("IX_RECALL_APPLY_ID");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");
            builder.Property(t => t.ArchivalId).HasColumnName("ARCHIVAL_ID").HasMaxLength(50).HasComment("档案号");
            builder.Property(t => t.RecallApplyId).HasColumnName("RECALL_APPLY_ID").HasMaxLength(50).HasComment("召回申请ID");
            builder.HasOne(t => t.RecallApply).WithMany(t => t.RecallApplyAndArchivals).HasForeignKey(t => t.RecallApplyId).HasConstraintName("FK_RECALL_APPLY_ID");

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