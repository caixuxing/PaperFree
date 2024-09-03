using System.ComponentModel;

namespace Trasen.PaperFree.Domain.Shared.Enums
{
    /// <summary>
    /// 流程申请状态
    /// </summary>
    public enum ProcessStatusType
    {
        /// <summary>
        /// 待审批
        /// </summary>
        [Description("待审批")]
        AWAITAPPROVAL = 0,

        /// <summary>
        /// 审批中
        /// </summary>
        [Description("审批中")]
        UNDERAPPROVAL = 1,

        /// <summary>
        /// 结束
        /// </summary>
        [Description("结束")]
        END = 2,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        FINISH = 3,
    }
}