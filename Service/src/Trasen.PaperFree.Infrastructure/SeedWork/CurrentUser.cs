namespace Trasen.PaperFree.Infrastructure.SeedWork
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        ///
        /// </summary>
        /// <param name="accessor"></param>
        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual string Id
        {
            get
            {
                var id = _accessor?.HttpContext?.User?.FindFirst(ClaimAttributes.UserId);
                return id?.Value ?? "B03EF5E1-469A-B7A0-26D4-A38E9957C246";
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                var name = _accessor?.HttpContext?.User?.FindFirst(ClaimAttributes.UserName);
                return name?.Value ?? "admin";
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserNickName
        {
            get
            {
                var name = _accessor?.HttpContext?.User?.FindFirst(ClaimAttributes.UserNickName);
                return name?.Value ?? "管理员";
            }
        }

        /// <summary>
        /// 机构
        /// </summary>
        public string OrgCode
        {
            get
            {
                var orgcode = _accessor?.HttpContext?.User?.FindFirst(ClaimAttributes.OrgCode);
                return orgcode?.Value ?? "org_001";
            }
        }

        /// <summary>
        /// 院区
        /// </summary>
        public string HospCode
        {
            get
            {
                var hospcode = _accessor?.HttpContext?.User?.FindFirst(ClaimAttributes.HospCode);
                return hospcode?.Value ?? "hosp_001";
            }
        }

        /// <summary>
        /// 院区
        /// </summary>
        public string InputCode
        {
            get
            {
                var inputCode = _accessor?.HttpContext?.User?.FindFirst(ClaimAttributes.InputCode);
                return inputCode?.Value ?? "input_001";
            }
        }
    }
}