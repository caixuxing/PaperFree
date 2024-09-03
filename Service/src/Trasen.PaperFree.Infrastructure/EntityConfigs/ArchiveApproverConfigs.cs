using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 归档审批记录表
    /// </summary>
    public class ArchiveApproverConfigs : IEntityTypeConfiguration<ArchiveApprover>
    {
        public void Configure(EntityTypeBuilder<ArchiveApprover> builder)
        {
            builder.ToTable("ARCHIVE_APPROVER").HasComment("归档审批记录表");
            builder.HasIndex(x => x.ArchiveApplyId).HasDatabaseName("IX_ARCHIVE_APPLY_ID");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever().HasComment("主键ID");
            builder.Property(t => t.ArchiveApplyId).HasColumnName("ARCHIVE_APPLY_ID").HasMaxLength(50).HasComment("归档申请ID");
            builder.HasOne(s => s.ArchiveApply).WithMany(g => g.ArchiveApprovers).HasForeignKey(s => s.ArchiveApplyId)
                .HasConstraintName("FK_ARCHIVE_APPLY_ID");
            builder.Property(x => x.WorkFlowStatus).HasColumnName("Work_Flow_Status")
           .HasConversion(x => x.ToString(), x => (WorkFlowState)Enum.Parse(typeof(WorkFlowState), x))
           .HasMaxLength(20).HasComment("流程状态");
            builder.Property(t => t.ApprovalResult).HasColumnName("APPROVAL_RESULT")
                 .HasConversion(x => x.ToString(), x => (EventDirectionType)Enum.Parse(typeof(EventDirectionType), x))
                .HasMaxLength(50).HasComment("审批结果");
            builder.Property(t => t.ApprovalRemark).HasColumnName("APPROVAL_REMARK").HasMaxLength(300).HasComment("审批备注");
            builder.Property(t => t.ApprovalId).HasColumnName("APPROVAL_ID").HasMaxLength(50).HasComment("审批人ID");
            builder.Property(t => t.approvalDateTime).HasColumnName("APPROVAL_DATE_TIME").HasComment("审批时间");
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