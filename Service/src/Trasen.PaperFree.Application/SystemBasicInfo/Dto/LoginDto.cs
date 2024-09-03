namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto
{
    /// <summary>
    /// 创新登录成功后用户信息
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 账户号Code
        /// </summary>
        public string MEMBER_CODE { get; set; }

        /// <summary>
        /// 用户昵称（姓名）
        /// </summary>
        public string MEMBER_NAME { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        public string ORG_CODE { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string ORG_NAME { get; set; }

        /// <summary>
        /// 院区代码
        /// </summary>
        public string HOSP_CODE { get; set; }

        /// <summary>
        /// 院区名称
        /// </summary>
        public string HOSP_NAME { get; set; }

        public string ORG_LEVEL { get; set; }

        public string ADMIN_CODE { get; set; }

        public string ADMIN_NAME { get; set; }

        public string ORG_SHORT { get; set; }
    }
}