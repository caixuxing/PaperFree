using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trasen.PaperFree.Domain.Shared.Config;
using Trasen.PaperFree.Domain.Shared.Const;

namespace Trasen.PaperFree.Domain.Shared.Help
{
    /// <summary>
    /// jwt封装类
    /// </summary>
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="jwtSetting"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(JwtSetting jwtSetting, TokenModelJwt tokenModel)
        {
            string iss = jwtSetting.Issuer;
            string aud = jwtSetting.AccessTokenAudience;
            string secret = jwtSetting.SecurityKey;
            int expires = jwtSetting.AccessTokenExpires;
            int refreshExpires = jwtSetting.RefreshTokenExpires;
            var timestamp = DateTime.Now.AddMinutes(expires + refreshExpires).ToString();
            var claims = new List<Claim>
                {
            new Claim(ClaimAttributes.UserId, tokenModel.UserId),
            new Claim(ClaimAttributes.UserName, tokenModel.UserLoginName),
            new Claim(ClaimAttributes.UserNickName, tokenModel.UserNickName),
            new Claim(ClaimAttributes.OrgCode, tokenModel.OrgCode),
            new Claim(ClaimAttributes.HospCode, tokenModel.HospCode)
            //,
            //new Claim(ClaimAttributes.RefreshExpires, timestamp)
            };

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                audience: aud,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(expires),
                signingCredentials: creds);
            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);
            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            TokenModelJwt tokenModelJwt = new TokenModelJwt();

            // token校验
            if (!string.IsNullOrWhiteSpace(jwtStr) && jwtHandler.CanReadToken(jwtStr))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                Claim[] claimArr = jwtToken?.Claims?.ToArray();
                if (claimArr != null && claimArr.Length > 0)
                {
                    tokenModelJwt.UserId = claimArr.FirstOrDefault(a => a.Type == ClaimAttributes.UserId)?.Value;
                    tokenModelJwt.UserLoginName = claimArr.FirstOrDefault(a => a.Type == ClaimAttributes.UserName)?.Value;
                    tokenModelJwt.UserNickName = claimArr.FirstOrDefault(a => a.Type == ClaimAttributes.UserNickName)?.Value;
                    tokenModelJwt.OrgCode = claimArr.FirstOrDefault(a => a.Type == ClaimAttributes.OrgCode)?.Value;
                    tokenModelJwt.HospCode = claimArr.FirstOrDefault(a => a.Type == ClaimAttributes.HospCode)?.Value;
                    tokenModelJwt.RefreshExpires = claimArr.FirstOrDefault(a => a.Type == ClaimAttributes.RefreshExpires)?.Value;
                }
            }
            return tokenModelJwt;
        }
    }

    public class TokenModelJwt
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserLoginName { get; set; }

        /// <summary>
        /// 用户昵称（姓名）
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 院区代码
        /// </summary>
        public string HospCode { get; set; }

        /// <summary>
        /// 刷新有效时间
        /// </summary>
        public string RefreshExpires { get; set; }
    }
}