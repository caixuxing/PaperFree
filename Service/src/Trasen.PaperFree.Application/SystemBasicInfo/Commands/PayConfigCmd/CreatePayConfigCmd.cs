namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.PayConfig
{
    public record CreatePayConfigCmd : IRequest<string>
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        public string ServiceProviders { get; set; }
        /// <summary>
        /// AppID
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantNumber { get; set; }
        /// <summary>
        /// 回调URL
        /// </summary>
        public string CallbackUrl { get; set; }
        /// <summary>
        /// 接口版本
        /// </summary>
        public string InterfaceVersion { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncryptionKey { get; set; }
        /// <summary>
        /// 支付成功通知模板
        /// </summary>
        public string Completionnotification { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get;  set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get;  set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get;  set; }
        public class CreatePayConfigCmdValidate : AbstractValidator<CreatePayConfigCmd>
        {
            public CreatePayConfigCmdValidate()
            {
                RuleFor(x => x.ServiceProviders).NotEmpty().WithMessage("服务提供者不能为空！")
                    .MaximumLength(30).WithMessage("服务提供者长度不能超过30个字符");
                RuleFor(x => x.AppId).NotEmpty().WithMessage("AppID不能为空");
                RuleFor(x => x.AppSecret).NotEmpty().WithMessage("AppSecret不能为空")
                .MaximumLength(40).WithMessage("接口版本长度不能超过40个字符");
                RuleFor(x => x.InterfaceVersion).NotEmpty().WithMessage("接口版本不能为空")
                     .MaximumLength(10).WithMessage("接口版本长度不能超过10个字符");
                RuleFor(x => x.CallbackUrl).NotEmpty().WithMessage("回调URL不能为空")
                  .MaximumLength(50).WithMessage("回调URL长度不能超过50个字符");
                RuleFor(x => x.EncryptionKey).NotEmpty().WithMessage("加密密钥不能为空")
               .MaximumLength(200).WithMessage("加密密钥长度不能超过200个字符");
                RuleFor(x => x.Token).NotEmpty().WithMessage("加密密钥不能为空")
               .MaximumLength(200).WithMessage("Token长度不能超过200个字符");

                RuleFor(x => x.MerchantNumber).NotEmpty().WithMessage("商户号不能为空")
               .MaximumLength(200).WithMessage("商户号长度不能超过200个字符");
                RuleFor(x => x.Completionnotification).NotEmpty().WithMessage("支付成功模板不能为空")
              .MaximumLength(50).WithMessage("支付成功模板长度不能超过50个字符");
                RuleFor(x => x.OrgCode).MaximumLength(50).WithMessage("机构编码长度不能超过").NotEmpty().WithMessage("机构编码不能为空");
                RuleFor(x => x.HospCode).MaximumLength(50).WithMessage("院区编码长度不能超过").NotEmpty().WithMessage("院区编码不能为空");
                RuleFor(x => x.InputCode).MaximumLength(50).WithMessage("辖区编码长度不能超过").NotEmpty().WithMessage("辖区编码不能为空");
            }
        }
    }
}