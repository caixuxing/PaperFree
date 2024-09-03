using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 水印表
    /// </summary>
    internal class BaseWatermarkConfigs : IEntityTypeConfiguration<BaseWatermark>
    {
        public void Configure(EntityTypeBuilder<BaseWatermark> builder)
        {
            builder.ToTable("BASE_WATERMARK").HasComment("水印表");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("WATERMARK_ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");
            builder.Property(x => x.WatermarkName).HasColumnName("WATERMARK_NAME").HasMaxLength(100).HasComment("水印名称");
            builder.Property(x => x.UseScene).HasColumnName("USE_SCENE").HasMaxLength(20).HasComment("使用场景");
            builder.Property(x => x.Color).HasColumnName("COLOR").HasMaxLength(100).HasComment("颜色");
            builder.Property(x => x.Big).HasColumnName("BIG").HasMaxLength(20).HasComment("大小");
            builder.Property(x => x.Xstation).HasColumnName("XSTATION").HasMaxLength(10).HasComment("x坐标");
            builder.Property(x => x.Ystation).HasColumnName("YSTATION").HasMaxLength(10).HasComment("y坐标");
            builder.Property(x => x.Angle).HasColumnName("ANGLE").HasMaxLength(10).HasComment("角度");
            builder.Property(x => x.Style).HasColumnName("STYLE").HasMaxLength(10).HasComment("样式");
            builder.Property(x => x.Direction).HasColumnName("DIRECTION").HasMaxLength(10).HasComment("方向");
            builder.Property(x => x.Font).HasColumnName("FONT").HasMaxLength(10).HasComment("字体");
            builder.Property(x => x.FontSize).HasColumnName("FONTSIZE").HasMaxLength(10).HasComment("字体大小");

            builder.Property(x => x.Pellucidity).HasColumnName("PELLUCIDITY").HasMaxLength(10).HasComment("透明度");
            builder.Property(x => x.Hight).HasColumnName("HIGHT").HasMaxLength(10).HasComment("高度");
            builder.Property(x => x.Width).HasColumnName("WIDTH").HasMaxLength(10).HasComment("宽度");
            builder.Property(x => x.IsSuitable).HasColumnName("IS_SUITABLE").HasMaxLength(10).HasComment("是否合适大小");
            builder.Property(x => x.Picture).HasColumnType("CLOB").HasColumnName("PICTURE").HasComment("图片");
            builder.Property(x => x.PicX).HasColumnName("PICX").HasMaxLength(10).HasComment("PicX");
            builder.Property(x => x.PicY).HasColumnName("PICY").HasMaxLength(10).HasComment("PicY");
            builder.Property(x => x.GapX).HasColumnName("GAPX").HasMaxLength(10).HasComment("左右间隔");
            builder.Property(x => x.GapY).HasColumnName("GAPY").HasMaxLength(10).HasComment("上下间隔");
            builder.Property(x => x.Status).HasColumnName("STATUS").HasMaxLength(10).HasComment("状态");
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