using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{/// <summary>
/// 归档申请
/// </summary>
    public class ArchiveApplyConfigs : IEntityTypeConfiguration<ArchiveApply>
    {
        public void Configure(EntityTypeBuilder<ArchiveApply> builder)
        {
            builder.ToTable("ARCHIVE_APPLY").HasComment("归档申请");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever().HasComment("主键ID");
            builder.Property(t => t.ArchiveId).HasColumnName("ARCHIVAL_ID").HasMaxLength(50).HasComment("档案号");
            builder.Property(t => t.ArchiveName).HasColumnName("ARCHIVE_NAME").HasMaxLength(50).HasComment("申请名称,默认为当前病案人归档作为名称，批量为批量归档申请");
            builder.Property(t => t.CurrentStatus).HasColumnName("CURRENT_STATUS").HasMaxLength(10).HasComment("状态 流程状态：开始》审批中》结束 、作废");
            builder.Property(t => t.IsEnd).HasColumnName("IS_END").HasComment("流程是否结束 状态：是 、否");
            builder.Property(t => t.ProcessDesignId).HasColumnName("PROCESS_DESIGN_ID").HasMaxLength(50).HasComment("流程模板ID 绑定当前当前申请审批模板");
            builder.Property(t => t.CurrentApprovalNodeId).HasColumnName("CURRENT_APPROVAL_NODE_ID").HasMaxLength(50).HasComment("当前审批节点ID ");
            builder.Property(t => t.CurrentApprovalNodeName).HasColumnName("CURRENT_APPROVAL_NODE_NAME").HasMaxLength(50).HasComment("当前审批节点名称");
            builder.Property(t => t.NodeApprovaName).HasColumnName("NODE_APPROVA_NAME").HasMaxLength(1000).HasComment("当前节点审批人名称");
            builder.Property(t => t.ApplyPersonName).HasColumnName("APPLY_PERSON_NAME").HasMaxLength(20).HasComment("申请人姓名");
            builder.Property(t => t.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("机构编码");
            builder.Property(t => t.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50).HasComment("院区编码");
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