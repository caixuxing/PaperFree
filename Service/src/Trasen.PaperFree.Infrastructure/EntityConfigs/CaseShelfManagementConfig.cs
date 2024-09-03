using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 纸质病例存储管理
    /// </summary>
    internal class CaseShelfManagementConfig : IEntityTypeConfiguration<CaseShelfManagement>
    {
        public void Configure(EntityTypeBuilder<CaseShelfManagement> builder)
        {
            builder.ToTable("CASE_SHELF_MANAGEMENT").HasComment("纸质病例存储管理");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");
            builder.Property(x => x.WarehouseNumber).HasColumnName("WAREHOUSE_NUMBER").HasMaxLength(20).HasComment("仓库编号");
            builder.Property(x => x.WarehouseName).HasColumnName("WAREHOUSE_NAME").HasMaxLength(20).HasComment("仓库名称");
            builder.Property(x => x.ShelfNo).HasColumnName("SHELF_NO").HasMaxLength(20).HasComment("病架号");
            builder.Property(x => x.StorageNumberSegment).HasColumnName("STORAGE_NUMBER_SEGMENT").HasMaxLength(20).HasComment("存储号段");
            builder.Property(x => x.LineNumber).HasColumnName("LINE_NUMBER").HasMaxLength(10).HasComment("行数");
            builder.Property(x => x.NumberOlumns).HasColumnName("NUMBER_OLUMNS").HasMaxLength(10).HasComment("列数");
            builder.Property(x => x.Status).HasColumnName("STATUS").HasMaxLength(10).HasComment("状态");
            builder.Property(x => x.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("机构编码");
            builder.Property(x => x.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50).HasComment("院区编码");
            builder.Property(x => x.InputCode).HasColumnName("INPUT_CODE").HasMaxLength(50).HasComment("辖区编码");

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