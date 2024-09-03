using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.RecallRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Enums;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 召回申请表
    /// </summary>
    public class RecallApplyConfigs : IEntityTypeConfiguration<RecallApply>
    {
        public void Configure(EntityTypeBuilder<RecallApply> builder)
        {
            builder.ToTable("RECALL_APPLY").HasComment("召回申请");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");
            builder.Property(t => t.ArchiveId).HasColumnName("ARCHIVE_ID").HasMaxLength(50).HasComment("档案号");
            builder.Property(t => t.ApplyName).HasColumnName("APPLY_NAME").HasMaxLength(30).HasComment("申请名称");
            builder.Property(t => t.ApplyReason).HasColumnName("APPLY_REASON").HasMaxLength(200).HasComment("申请原因");
            builder.Property(t => t.AttachmentMaterials).HasColumnName("ATTACHMENT_MATERIALS").HasMaxLength(200).HasComment("附件材料");
            builder.Property(t => t.CurrentStatus).HasColumnName("CURRENT_STATUS").HasMaxLength(20)
                .HasConversion(x => x.ToString(), x => (ProcessStatusType)Enum.Parse(typeof(ProcessStatusType), x))
                .HasComment("状态");
            builder.Property(t => t.IsEnd).HasColumnName("IS_END").HasComment("是否结束");
            builder.Property(t => t.ProcessDesignId).HasColumnName("PROCESS_DESIGN_ID").HasMaxLength(50).HasComment("流程模板ID");
            builder.Property(t => t.CurrentApprovalNodeId).HasColumnName("CURRENT_APPROVAL_NODE_ID").HasMaxLength(50).HasComment("当前审批节点ID");
            builder.Property(t => t.CurrentApprovalNodeName).HasColumnName("CURRENT_APPROVAL_NODE_NAME").HasMaxLength(50).HasComment("当前审批节点名称");
            builder.Property(t => t.NodeApprovalName).HasColumnName("NODE_APPROVAL_NAME").HasMaxLength(1000).HasComment("当前节点审批人名称");
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