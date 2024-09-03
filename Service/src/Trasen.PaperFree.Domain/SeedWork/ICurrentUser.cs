namespace Trasen.PaperFree.Domain.SeedWork
{
    public interface ICurrentUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserNickName { get; }

        /// <summary>
        ///
        /// </summary>
        string OrgCode { get; }

        /// <summary>
        ///
        /// </summary>
        string HospCode { get; }

        /// <summary>
        ///
        /// </summary>
        string InputCode { get; }
    }
}