using Trasen.PaperFree.Application.MedicalRecord.Dto.Archive;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.Archive;

/// <summary>
/// 审批详细Qry
/// </summary>
/// <param name="Id"></param>
public record FindArchiveApplyIdQry(string Id):IRequest<ArchiveApplyDetailDto>;