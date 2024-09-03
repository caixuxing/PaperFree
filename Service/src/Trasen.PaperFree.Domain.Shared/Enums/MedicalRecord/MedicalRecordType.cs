using System.ComponentModel;

namespace Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord
{
    public enum MedicalRecordType
    {
        /// <summary>
        /// 待提交
        /// </summary>
        [Description("待提交")]
        AWAITCOMMIT = 0,

        /// <summary>
        /// 已归档
        /// </summary>
        [Description("已归档")]
        ARCHIVECOMPLETE = 10,

        /// <summary>
        /// 已作废
        /// </summary>
        [Description("已作废")]
        CANCEL = 20,
    }
}