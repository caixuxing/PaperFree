using Trasen.PaperFree.Application.MedicalRecord.Dto.Recall;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.Recall;

/// <summary>
/// 召回审批详细Qry
/// </summary>
/// <param name="Id"></param>
public record FindRecallApplyIdQry(string Id) : IRequest<RecallApplyDetailDto>;