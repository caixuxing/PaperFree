namespace Trasen.PaperFree.Domain.Shared.Config;

/// <summary>
/// 创星基础平台配置信息
/// </summary>
public class TrasenBasePlatformSetting
{
    /// <summary>
    /// 基础数据服务接口域名
    /// </summary>
    public string domainName { get; set; }

    /// <summary>
    /// 病案系统域名
    /// </summary>
    public string MedicalDomainName { get; set; }
}