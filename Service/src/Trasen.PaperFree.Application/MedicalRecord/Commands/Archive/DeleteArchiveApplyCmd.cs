namespace Trasen.PaperFree.Application.MedicalRecord.Commands.Archive;

/// <summary>
/// 删除归档申请Cmd
/// </summary>
/// <param name="id"></param>
public record DeleteArchiveApplyCmd(string id) : IRequest<bool>;