using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    internal class DeptMenuTreeConfigs : IEntityTypeConfiguration<DeptMeMenuTreeEntity>
    {
        public void Configure(EntityTypeBuilder<DeptMeMenuTreeEntity> builder)
        {
            builder.ToTable("DEPT_MEUM_TREE").HasComment("必传文件配置");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");
            builder.Property(x => x.DeptId).HasColumnName("DEPT_ID").HasMaxLength(100).HasComment("科室ID");
            builder.Property(x => x.ArchiverMeumId).HasColumnName("ARCHIVER_MEUM_ID").HasMaxLength(50).HasComment("归档目录模板ID");
            builder.Property(x => x.ParentId).HasColumnName("PARENT_ID").HasMaxLength(50).HasComment("上级目录ID");
            builder.Property(x => x.IsRequired).HasColumnName("IS_REQUIRED")
                  .HasConversion(x => x.ToString(), x => (WhetherType)Enum.Parse(typeof(WhetherType), x)).HasMaxLength(20).HasComment("是否必填");
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