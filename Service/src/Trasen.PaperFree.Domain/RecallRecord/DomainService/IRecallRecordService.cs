using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.ArchiveRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.RecallRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.RecallRecord.DomainService
{
    /// <summary>
    /// 召回领域服务
    /// </summary>
    public interface IRecallRecordService
    {
        /// <summary>
        /// 流程事件状态流
        /// </summary>
        /// <returns></returns>
        public WorkFlowState ProcessEnventStatuFlow(bool IsRejectToNode, string RejectNodeId, EventDirectionType eventDirectionType,
            ProcessDesign processDesign, RecallApply archiveApply);
    }
}
