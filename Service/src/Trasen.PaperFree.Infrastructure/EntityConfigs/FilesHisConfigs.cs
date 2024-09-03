using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.FileTable.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    public class FilesHisConfigs : IEntityTypeConfiguration<FilesHis>
    {
        public void Configure(EntityTypeBuilder<FilesHis> builder)
        {
            builder.ToTable("FILES_HIS").HasComment("HIS文件表");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");
            builder.Property(t => t.ArchiveId).HasColumnName("ARCHIVE_ID").HasMaxLength(50).HasComment("档案号ID");
            builder.Property(t => t.FileId).HasColumnName("FILE_ID").HasMaxLength(50).HasComment("文件唯-ID");
            builder.Property(t => t.MeumId).HasColumnName("MEUM_ID").HasMaxLength(50).HasComment("病历目录表ID");
            builder.Property(t => t.FileTruename).HasColumnName("FILE_TRUENAME").HasMaxLength(200).HasComment("文件原名称");
            builder.Property(t => t.FileSavename).HasColumnName("FILE_SAVENAME").HasMaxLength(200).HasComment("文件保存后名称");
            builder.Property(t => t.FileType).HasColumnName("FILE_TYPE").HasMaxLength(10).HasComment("文件类型格式");

            builder.Property(t => t.FilePath).HasColumnName("FILE_PATH").HasMaxLength(200).HasComment("文件存储路径");
            builder.Property(t => t.FileSize).HasColumnName("FILE_SIZE").HasMaxLength(10).HasComment("文件大小");
            builder.Property(t => t.OrderNo).HasColumnName("ORDER_NO").HasMaxLength(10).HasComment("排序号");
            builder.Property(t => t.Status).HasColumnName("STATUS").HasMaxLength(10).HasComment("状态【物理文件上传状态】");
            builder.Property(t => t.StorageState).HasColumnName("STORAGE_STATE").HasMaxLength(10).HasComment("文件存储状态【临时、正式】");
            builder.Property(t => t.DataType).HasColumnName("DATA_TYPE").HasMaxLength(10).HasComment("数据类型 1 门诊 2 住院");

            builder.Property(t => t.Remark).HasColumnName("REMARK").HasMaxLength(200).HasComment("备注");
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