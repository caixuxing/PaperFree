using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    public class SysOperLogConfig : IEntityTypeConfiguration<SysOperLog>
    {
        public void Configure(EntityTypeBuilder<SysOperLog> builder)
        {
            builder.ToTable("SYS_OPER_LOG").HasComment("系统操作日志");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("ID").HasMaxLength(50).HasComment("主键ID").ValueGeneratedNever();
            builder.Property(x => x.Title).HasColumnName("TITLE").HasMaxLength(20).HasComment("操作模块");
            builder.Property(x => x.BusinessType).HasColumnName("BUSINESS_TYPE")
                 .HasMaxLength(20)
                .HasConversion(x => x.ToString(), x => (BusinessType)Enum.Parse(typeof(BusinessType), x))
                .HasComment("业务类型");
            builder.Property(x => x.RequestMethod).HasColumnName("REQUEST_METHOD").HasMaxLength(100).HasComment("请求方法名");
            builder.Property(x => x.RequestType).HasColumnName("REQUEST_TYPE").HasMaxLength(10).HasComment("请求方式");
            builder.Property(x => x.OperatorType).HasColumnName("OPERATOR_TYPE")
                  .HasConversion(x => x.ToString(), x => (BperatorType)Enum.Parse(typeof(BperatorType), x))
                    .HasMaxLength(20)
                .HasComment("操作类别");
            builder.Property(x => x.OperName).HasColumnName("OPER_NAME").HasMaxLength(20).HasComment("操作人");
            builder.Property(x => x.RequestUrl).HasColumnName("REQUEST_URL").HasMaxLength(100).HasComment("请求Url");
            builder.Property(x => x.OperIp).HasColumnName("OPER_IP").HasMaxLength(20).HasComment("操作IP地址");
            builder.Property(x => x.OperAddr).HasColumnName("OPER_ADDR").HasMaxLength(50).HasComment("操作地址");
            builder.Property(x => x.RequestParam).HasColumnName("REQUEST_PARAM").HasComment("请求参数").HasColumnType("CLOB");
            builder.Property(x => x.JsonResult).HasColumnName("JSON_RESULT").HasComment("返回参数").HasColumnType("CLOB");
            builder.Property(x => x.Status).HasColumnName("STATUS")
                    .HasConversion(x => x.ToString(), x => (StatusLogType)Enum.Parse(typeof(StatusLogType), x))
                    .HasMaxLength(20)
                .HasComment("操作状态");
            builder.Property(x => x.ErrorMsg).HasColumnName("ERROR_MSG").HasComment("错误消息");
            builder.Property(x => x.OperTime).HasColumnName("OPER_TIME").HasComment("操作时间");
            builder.Property(x => x.Elapsed).HasColumnName("ELAPSED").HasComment("操作用时");
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