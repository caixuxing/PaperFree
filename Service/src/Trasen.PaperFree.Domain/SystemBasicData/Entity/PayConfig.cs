using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 支付配置表
    /// </summary>
    public record PayConfig : FullRoot
    {
        private PayConfig() { }
        public PayConfig(string id, string serviceProviders, int appId, string appSecret, string merchantNumber, string callbackUrl, string interfaceVersion, string token,
           string encryptionKey, string completionnotification, int isEnable, string orgcode, string hospcode, string inputcode)
        {
            Id = id;
            ServiceProviders = serviceProviders;
            AppId = appId;
            AppSecret = appSecret;
            MerchantNumber = merchantNumber;
            CallbackUrl = callbackUrl;
            InterfaceVersion = interfaceVersion;
            Token = token;
            EncryptionKey = encryptionKey;
            Completionnotification = completionnotification;
            IsEnable = isEnable;
            OrgCode = orgcode;
            HospCode = hospcode;
            InputCode = inputcode;
        }
        public PayConfig UpdatePayConfig(string serviceProviders, int appId, string appSecret, string merchantNumber, string callbackUrl, string interfaceVersion, string token,
           string encryptionKey, string completionnotification, int isEnable, string orgcode, string hospcode, string inputcode)
        {
            this.ServiceProviders = serviceProviders;
            this.AppId = appId;
            this.AppSecret = appSecret;
            this.MerchantNumber = merchantNumber;
            this.CallbackUrl = callbackUrl;
            this.InterfaceVersion = interfaceVersion;
            this.Token = token;
            this.EncryptionKey = encryptionKey;
            this.Completionnotification = completionnotification;
            this.IsEnable = isEnable;
            this.OrgCode = orgcode;
            this.HospCode = hospcode;
            this.InputCode = inputcode;
            return this;
        }
        /// <summary>
        /// 服务提供者
        /// </summary>
        public string ServiceProviders { get; private set; }
        /// <summary>
        /// AppID
        /// </summary>
        public int AppId { get; private set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; private set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantNumber { get; private set; }
        /// <summary>
        /// 回调URL
        /// </summary>
        public string CallbackUrl { get; private set; }
        /// <summary>
        /// 接口版本
        /// </summary>
        public string InterfaceVersion { get; private set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; private set; }
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncryptionKey { get; private set; }
        /// <summary>
        /// 支付成功通知模板
        /// </summary>
        public string Completionnotification { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; private set; }
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        /// <summary>
        /// 辖区
        /// </summary>
        public string InputCode { get; set; }
    }
}