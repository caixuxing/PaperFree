namespace Trasen.PaperFree.Domain.Shared.Enums.ArchiveRecord
{
    /// <summary>
    /// 归档流程申请枚举
    /// </summary>
    public enum ArchiveApplyStatusType
    {
        /// <summary>
        /// 作废
        /// </summary>
        INVALID = -1,

        /// <summary>
        /// 开始
        /// </summary>
        START = 0,

        /// <summary>
        /// 审批中
        /// </summary>
        APPROVAL = 1,

        /// <summary>
        /// 结束
        /// </summary>
        END = 2
    }
}