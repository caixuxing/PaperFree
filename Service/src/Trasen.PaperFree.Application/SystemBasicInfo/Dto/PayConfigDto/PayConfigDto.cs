namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.PayConfigDto
{
    public record PayConfigDto
    {
        public string Id { get; set; }
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