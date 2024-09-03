using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 必传文件配置
    /// </summary>
    internal class EssentialDocumentsConfigs : IEntityTypeConfiguration<EssentialDocuments>
    {
        public void Configure(EntityTypeBuilder<EssentialDocuments> builder)
        {
            builder.ToTable("ESSENTIAL_DOCUMENTS").HasComment("必传文件配置");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");

            builder.Property(t => t.DeptCode).HasColumnName("DEPT_CODE").HasMaxLength(100).HasComment("科室编码");
            builder.Property(t => t.FatherMeumid).HasColumnName("FATHER_MEUMID").HasMaxLength(50).HasComment("目录编码");
            builder.Property(t => t.MeumType).HasColumnName("MEUM_TYPE").HasMaxLength(10).HasComment("目录类型");
            builder.Property(t => t.Status).HasColumnName("STATUS").HasMaxLength(10).HasComment("状态");
            builder.Property(t => t.OrderId).HasColumnName("ORDERID").HasMaxLength(10).HasComment("排序");
            builder.Property(x => x.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("使用机构");
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