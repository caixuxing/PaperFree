namespace Trasen.PaperFree.Application.SystemBasicInfo.Query
{
    /// <summary>
    /// 登录请求入参
    /// </summary>
    /// <param name="loginName">登录名</param>
    /// <param name="loginpassword">登录密码</param>
    /// <param name="orgCode">机构代码</param>
    /// <param name="hospCode">院区代码</param>
    public record LoginQry(string loginName, string loginpassword, string orgCode, string hospCode) : IRequest<string>;

    /// <summary>
    /// 登录指令属性校验
    /// </summary>
    public class UserQueryValidator : AbstractValidator<LoginQry>
    {
        /// <summary>
        ///验证规则
        /// </summary>
        public UserQueryValidator()
        {
            RuleFor(x => x.loginName).NotEmpty().WithMessage("登录名不能为空！");
            RuleFor(x => x.loginpassword).NotEmpty().WithMessage("登录密码不能为空！");
            RuleFor(x => x.orgCode).NotEmpty().WithMessage("机构代码不能为空！");
            RuleFor(x => x.hospCode).NotEmpty().WithMessage("院区代码不能为空！");
        }
    }
}