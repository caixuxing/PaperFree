using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Dto.Archive;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.Recall
{
    /// <summary>
    /// 召回审批时间轴Dto
    /// </summary>
    public class RecallApprovalTimelineDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string AdmissId { get; set; }

        /// <summary>
        /// 审批状态流转信息
        /// </summary>
        public List<ApprovalStatusFlowValueObj> approvalStatusFlows { get; set; } = new List<ApprovalStatusFlowValueObj>();
    }
}
