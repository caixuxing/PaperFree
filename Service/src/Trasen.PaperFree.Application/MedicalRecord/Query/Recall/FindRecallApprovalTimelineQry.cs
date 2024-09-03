using Trasen.PaperFree.Application.MedicalRecord.Dto.Recall;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.Recall;

/// <summary>
/// 召回审批时间抽Qry
/// </summary>
/// <param name="RecallApplyId">归档申请ID</param>
public record FindRecallApprovalTimelineQry(string RecallApplyId) : IRequest<RecallApprovalTimelineDto>;