using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 归档菜单目录
    /// </summary>
    public record ArchiverMeumConfigs : IEntityTypeConfiguration<ArchiverMeum>
    {
        public void Configure(EntityTypeBuilder<ArchiverMeum> builder)
        {
            builder.ToTable("ARCHIVER_MEUM").HasComment("归档菜单目录");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                    .HasComment("主键ID");
            builder.Property(t => t.MenuName).HasColumnName("MENU_NAME").HasMaxLength(30).HasComment("目录名称");
            builder.Property(t => t.ParentId).HasColumnName("PARENT_ID").HasMaxLength(50).HasComment("上级目录ID");
            builder.Property(t => t.Permission).HasColumnName("PERMISSION").HasMaxLength(20).HasComment("根据权限设置医护所能操作的目录");
            builder.Property(t => t.MeumType).HasColumnName("MEUM_TYPE").HasMaxLength(4).HasComment("目录类型（1、目录，2、节点）");
            builder.Property(t => t.SecretLevel).HasColumnName("SECRET_LEVEL").HasMaxLength(4).HasComment("保密等级");
            builder.Property(t => t.IsHighShots).HasColumnName("IS_HIGHSHOTS")
                   .HasConversion(x => x.ToString(), x => (WhetherType)Enum.Parse(typeof(WhetherType), x)).HasComment("是否高拍");
            builder.Property(t => t.IsSignature).HasColumnName("IS_SIGNATURE")
                   .HasConversion(x => x.ToString(), x => (WhetherType)Enum.Parse(typeof(WhetherType), x)).HasComment("是否签名");
            builder.Property(t => t.IsAllorg).HasColumnName("IS_ALLORG")
                   .HasConversion(x => x.ToString(), x => (WhetherType)Enum.Parse(typeof(WhetherType), x)).HasComment("是否适用所有机构（0、否；1、是）");
            builder.Property(t => t.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("机构编码");
            builder.Property(t => t.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50).HasComment("院区编码");
            builder.Property(t => t.Orderby).HasColumnName("ORDERBY").HasComment("排序");
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