namespace Trasen.PaperFree.Application.MedicalRecord.Commands.Recall;

/// <summary>
/// 删除召回申请
/// </summary>
public record DeleteRecallApplyCmd(string id) : IRequest<bool>;