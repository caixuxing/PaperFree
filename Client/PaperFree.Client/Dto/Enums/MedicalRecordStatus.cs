using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Dto.Enums
{
    public enum MedicalRecordStatus
    {
        /// <summary>
        /// 待提交
        /// </summary>
        [Description("待提交")]
        待提交,
        [Description("待签收")]
        待签收,
    }
}
