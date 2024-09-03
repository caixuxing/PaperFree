namespace Trasen.PaperFree.Domain.Shared.Const
{
    /// <summary>
    /// 创星授权方法常量
    /// </summary>
    public class TrasenApiConst
    {
        /// <summary>
        /// 登录
        /// </summary>

        public const string Login = "/BaseMenuUserlogin/UserLoginVerify";

        /// <summary>
        /// 用户菜单
        /// </summary>
        public const string UserMenu = "/BaseMenuUserlogin/getUserMenuInfor";

        /// <summary>
        /// 机构
        /// </summary>
        public const string Org = "/BasOrgOrganization/GetBasOrgOrganizationPageList";

        /// <summary>
        /// 科室
        /// </summary>
        public const string OrgDepartment = "/BasOrgDepartment/GetBasOrgDepartmentPageList";

        /// <summary>
        /// 根据ID获取系统参数
        /// </summary>
        public const string SysParameterList = "/SysParameter/GetSysParameterListByPage";

        /// <summary>
        /// 根据ID获取个人参数
        /// </summary>
        public const string PersonalParameterList = "/BasePersonalParameter/GetListByPage";

        /// <summary>
        /// 获取机构参数
        /// </summary>
        public const string OrgParameterPageList = "/BaseOrgParameter/GetPageList";

        /// <summary>
        /// 获取单个人员与科室关系表
        /// </summary>
        public const string OrgDeptMemberPageList = "/BasOrgDeptMember/GetBasOrgDeptMemberPageList";

        /// <summary>
        /// 获取人员信息表
        /// </summary>
        public const string OrgMemberPageList = "/BasOrgMember/QueryByPageBa";
        /// <summary>
        /// 获取行政区划
        /// </summary>
        public const string BasAdminDivisionList="/BasAdminDivision/GetBasAdminDivisionParameterPageList";
    }
}