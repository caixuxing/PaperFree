namespace Trasen.PaperFree.Domain.Shared.Config;

/// <summary>
/// JWT配置
/// </summary>
public class JwtSetting
{
    /// <summary>
    /// 发行者
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 接收
    /// </summary>
    public string AccessTokenAudience { get; set; }

    /// <summary>
    /// 秘钥key
    /// </summary>
    public string SecurityKey { get; set; }

    /// <summary>
    /// token过期时间
    /// </summary>
    public int AccessTokenExpires { get; set; }

    /// <summary>
    /// refresh Token
    /// </summary>
    public string RefreshTokenAudience { get; set; }

    /// <summary>
    /// refresh Token  exprise
    /// </summary>
    public int RefreshTokenExpires { get; set; }
}