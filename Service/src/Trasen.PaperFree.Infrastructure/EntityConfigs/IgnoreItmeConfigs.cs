using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.FileTable.Entity;
using Trasen.PaperFree.Domain.IgnoreItme.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    public class  IgnoreItmeConfigs : IEntityTypeConfiguration<IgnoreItmeTable>
    {
        public void Configure(EntityTypeBuilder<IgnoreItmeTable> builder)
        {
            builder.ToTable("IGNORE_ITME").HasComment("科室档案目录必填项忽略");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");
            builder.Property(a => a.ArchiveId).HasColumnName("ARCHIVE_ID").HasMaxLength(50).HasComment("档案号");
            builder.Property(a => a.MeumTreeId).HasColumnName("MEUM_TREE_ID").HasMaxLength(50).HasComment("必传文件配置ID");

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
