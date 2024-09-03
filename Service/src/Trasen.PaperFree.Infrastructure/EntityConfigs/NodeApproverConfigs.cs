using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 节点审批人员表
    /// </summary>
    public class NodeApproverConfigs : IEntityTypeConfiguration<NodeApprover>
    {
        public void Configure(EntityTypeBuilder<NodeApprover> builder)
        {
            builder.ToTable("NODE_APPROVER").HasComment("节点审批人员表");
            builder.HasIndex(x => x.ProcessNodeId).HasDatabaseName("IX_APPROVER_ID");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever().HasComment("主键ID");
            builder.Property(x => x.ProcessNodeId).HasColumnName("PROCESS_NODE_ID").HasMaxLength(50).HasComment("节点ID");
            builder.HasOne(x => x.ProcessNode).WithMany(g => g.NodeApprovers).HasForeignKey(x => x.ProcessNodeId).HasConstraintName("FK_PROCESS_NODE_ID");
            builder.Property(x => x.ApproverId).HasColumnName("APPROVER_ID").HasMaxLength(50).HasComment("审批人ID");
            builder.Property(x => x.ApproverAccount).HasColumnName("APPROVER_ACCOUNT").HasMaxLength(30).HasComment("审批人登录账户");
            builder.Property(x => x.ApproverName).HasColumnName("APPROVER_NAME").HasMaxLength(30).HasComment("审批人姓名");
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