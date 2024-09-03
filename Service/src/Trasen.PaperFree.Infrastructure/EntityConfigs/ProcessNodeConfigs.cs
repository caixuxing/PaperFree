using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 流程节点
    /// </summary>
    public class ProcessNodeConfigs : IEntityTypeConfiguration<ProcessNode>
    {
        public void Configure(EntityTypeBuilder<ProcessNode> builder)
        {
            builder.ToTable("PROCESS_NODE").HasComment("流程节点");
            builder.HasIndex(x => x.ProcessDesignId).HasDatabaseName("IX_PROCESS_DESIGN_ID");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever().HasComment("主键ID");
            builder.Property(x => x.ProcessDesignId).HasColumnName("PROCESS_DESIGN_ID").HasMaxLength(50).HasComment("流程主键ID");
            builder.HasOne(s => s.ProcessDesign).WithMany(g => g.ProcessNodes).HasForeignKey(s => s.ProcessDesignId).HasConstraintName("FK_PROCESS_DESIGN_ID");

            builder.Property(x => x.NodeName).HasColumnName("NODE_NAME").HasMaxLength(50).HasComment("节点名称");
            builder.Property(x => x.NodeCode).HasColumnName("NODE_CODE").HasMaxLength(20).HasComment("节点代码值");
            builder.Property(x => x.UpperNodeId).HasColumnName("UPPER_NODE_ID").HasMaxLength(50).HasComment("上级节点");
            builder.Property(x => x.LowerNodeId).HasColumnName("LOWER_NODE_ID").HasMaxLength(50).HasComment("下级节点");
            builder.Property(x => x.NodeMapWorkflowStatus).HasColumnName("NODE_MAP_WORKFLOW_STATUS").HasComment("节点对应流程状态");
            builder.Property(x => x.EventDirectionBranch).HasColumnName("EVENT_DIRECTION").HasMaxLength(50)
                .IsRequired(false).HasComment("事件方向 【拒绝、驳回、 通过】");
            builder.Property(x => x.IsRejectToNode).HasColumnName("IS_REJECT_TO_NODE").HasComment("是否可驳回指定节点");
            builder.Property(x => x.OderNo).HasColumnName("ODER_NO").HasComment("排序号");

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