using Trasen.PaperFree.Domain.ArchiveRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Enums.MedicalRecord;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.ArchiveRecord.DomainService
{
    /// <summary>
    /// 归档领域服务
    /// </summary>
    public interface IArchiveRecordService
    {
        /// <summary>
        /// 流程事件状态流
        /// </summary>
        /// <returns></returns>
        public WorkFlowState ProcessEnventStatuFlow(bool IsRejectToNode, string RejectNodeId, EventDirectionType eventDirectionType,
            ProcessDesign processDesign, ArchiveApply archiveApply);
    }
}