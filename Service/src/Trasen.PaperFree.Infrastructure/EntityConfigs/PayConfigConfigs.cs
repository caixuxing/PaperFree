using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 支付配置表
    /// </summary>
    internal class PayConfigConfigs : IEntityTypeConfiguration<PayConfig>
    {
        public void Configure(EntityTypeBuilder<PayConfig> builder)
        {
            builder.ToTable("PAY_CONFIG").HasComment("支付配置表");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasMaxLength(50).ValueGeneratedNever()
                .HasComment("主键ID");

            builder.Property(x => x.ServiceProviders).HasColumnName("SERVICE_PROVIDERS").HasMaxLength(30).HasComment("服务提供者");
            builder.Property(x => x.AppId).HasColumnName("APP_ID").HasMaxLength(40).HasComment("AppID");
            builder.Property(x => x.AppSecret).HasColumnName("APP_SECRET").HasMaxLength(40).HasComment("AppSecret");

            builder.Property(x => x.MerchantNumber).HasColumnName("MERCHANT_NUMBER").HasMaxLength(40).HasComment("商户号");
            builder.Property(x => x.CallbackUrl).HasColumnName("CALLBACK_URL").HasMaxLength(50).HasComment("回调URL");
            builder.Property(x => x.InterfaceVersion).HasColumnName("INTERFACE_VERSION").HasMaxLength(10).HasComment("接口版本");

            builder.Property(x => x.Token).HasColumnName("TOKEN").HasMaxLength(200).HasComment("Token");
            builder.Property(x => x.EncryptionKey).HasColumnName("ENCRYPTION_KEY").HasMaxLength(200).HasComment("加密密钥");
            builder.Property(x => x.Completionnotification).HasColumnName("COMPLETIONNOTIFICATION").HasMaxLength(50).HasComment("支付成功通知模板");

            builder.Property(x => x.OrgCode).HasColumnName("ORG_CODE").HasMaxLength(50).HasComment("机构编码");
            builder.Property(x => x.HospCode).HasColumnName("HOSP_CODE").HasMaxLength(50).HasComment("院区编码");
            builder.Property(x => x.InputCode).HasColumnName("INPUT_CODE").HasMaxLength(50).HasComment("辖区编码");

            builder.Property(x => x.IsEnable).HasColumnName("IS_ENABLE").HasComment("是否启用");

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