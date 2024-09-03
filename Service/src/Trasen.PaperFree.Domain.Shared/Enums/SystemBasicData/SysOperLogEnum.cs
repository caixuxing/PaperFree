using System.ComponentModel;

namespace Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData
{
    public enum BusinessType
    {
        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        OTHER = 0,

        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        ADD = 1,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        UPDATE = 2,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        DELETE = 3,

        /// <summary>
        /// 授权
        /// </summary>
        [Description("授权")]
        AUTHORIZATION = 4,

        /// <summary>
        /// 导出
        /// </summary>
        [Description("导出")]
        EXPORT = 5,

        /// <summary>
        /// 导入
        /// </summary>
        [Description("导入")]
        IMPORT = 6,

        /// <summary>
        /// 审批
        /// </summary>
        [Description("审批")]
        APPROVAL = 7,

        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        LOGIN = 8,

        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        GET = 9,
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum BperatorType
    {
        /// <summary>
        ///
        /// </summary>
        [Description("其它")]
        OTHER = 0,

        /// <summary>
        /// 后台
        /// </summary>
        [Description("后台")]
        BACKGROUND = 1,

        /// <summary>
        /// 手机
        /// </summary>
        [Description("手机")]
        MOBILE = 2
    }

    /// <summary>
    /// 日志状态
    /// </summary>
    public enum StatusLogType
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        NORMAL = 0,

        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        EXCEPTION = 1
    }
}