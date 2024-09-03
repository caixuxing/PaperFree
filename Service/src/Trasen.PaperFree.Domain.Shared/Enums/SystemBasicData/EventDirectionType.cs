using System.ComponentModel;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Attributes;

namespace Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData
{
    public enum EventDirectionType
    {
        /// <summary>
        ///结束
        /// </summary>
        [Description("结束")]
        [EnumSort(3)]
        END = 0,

        /// <summary>
        /// 通过
        /// </summary>
        [Description("通过")]
        [EnumSort(1)]
        PASS = 1,

        /// <summary>
        /// 驳回
        /// </summary>
        [Description("驳回")]
        [EnumSort(2)]
        REJECT = 2
    }
}