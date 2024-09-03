using Trasen.PaperFree.Application.MedicalRecord.Dto.Archive;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.Archive;

/// <summary>
/// 归档审批时间抽Qry
/// </summary>
/// <param name="ArchiveApplyId">归档申请ID</param>
public record FindApprovalTimelineQry(string ArchiveApplyId) : IRequest<ApprovalTimelineDto>;