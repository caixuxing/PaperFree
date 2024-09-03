namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;

/// <summary>
/// 删除流程设计指令
/// </summary>
public record DeleteProcessDesignCmd(string id) : IRequest<bool>;