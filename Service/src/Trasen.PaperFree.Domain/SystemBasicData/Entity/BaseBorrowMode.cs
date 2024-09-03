using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 借阅模板设置
    /// </summary>
    public record BaseBorrowMode : FullRoot
    {
        private BaseBorrowMode() { }
        public BaseBorrowMode(string modeName, string deptCode, string userCode, string isEnable, string orgCode, string hospCode, string inputCode)
        {
            ModeName = modeName;
            DeptCode = deptCode;
            UserCode = userCode;
            IsEnable = isEnable;

            OrgCode = orgCode;
            HospCode = hospCode;
            InputCode = inputCode;
        }
        public BaseBorrowMode ChangeBaseBorrowMode(string modeName, string deptCode, string userCode, string isEnable)
        {
            ModeName = modeName;
            DeptCode = deptCode;
            UserCode = userCode;
            IsEnable = isEnable;
            return this;
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string ModeName { get; private set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; private set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; private set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; private set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; private set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get; private set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public string IsEnable { get; private set; }
    }
}