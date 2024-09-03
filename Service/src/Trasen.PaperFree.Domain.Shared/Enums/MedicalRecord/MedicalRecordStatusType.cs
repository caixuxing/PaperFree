using System.ComponentModel;
using Trasen.PaperFree.Domain.Shared.Attributes;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord
{
    /// <summary>
    /// 工作流状态
    /// </summary>
    public enum WorkFlowState
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        [EnumParent((int)ProcessTempType.RECALL, (int)ProcessTempType.ARCHIVE)]
        NONE = -2,

        /// <summary>
        /// 召回待审核
        /// </summary>
        [Description("召回待审核")]
        [EnumParent((int)ProcessTempType.RECALL)]
        RECALLAWAITAUDIT = -1,

        /// <summary>
        /// 待提交
        /// </summary>
        [Description("待提交")]
        [EnumParent((int)ProcessTempType.RECALL, (int)ProcessTempType.ARCHIVE)]
        AWAITCOMMIT = 0,

        /// <summary>
        /// 已提交
        /// </summary>
        [Description("已提交")]
        [EnumParent((int)ProcessTempType.RECALL, (int)ProcessTempType.ARCHIVE)]
        ALREADYCOMMIT = 10,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        [EnumParent((int)ProcessTempType.RECALL, (int)ProcessTempType.ARCHIVE)]
        ALREADYAUDIT = 20,

        /// <summary>
        /// 已签收
        /// </summary>
        [Description("已签收")]
        [EnumParent((int)ProcessTempType.RECALL, (int)ProcessTempType.ARCHIVE)]
        ALREADYSIGN = 30,

        /// <summary>
        /// 已编目
        /// </summary>
        [Description("已编目")]
        [EnumParent((int)ProcessTempType.RECALL, (int)ProcessTempType.ARCHIVE)]
        ALREADYCATALOG = 40,

        /// <summary>
        /// 已归档
        /// </summary>
        [Description("已归档")]
        [EnumParent((int)ProcessTempType.RECALL, (int)ProcessTempType.ARCHIVE)]
        ALREADYARCHIVE = 50
    }
}