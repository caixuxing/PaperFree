using System.ComponentModel;

namespace Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData
{
    /// <summary>
    /// 流程模板类型
    /// </summary>
    public enum ProcessTempType
    {
        /// <summary>
        /// 归档
        /// </summary>
        [Description("归档")]
        ARCHIVE = 1,

        /// <summary>
        /// 召回
        /// </summary>
        [Description("召回")]
        RECALL = 2
    }
}