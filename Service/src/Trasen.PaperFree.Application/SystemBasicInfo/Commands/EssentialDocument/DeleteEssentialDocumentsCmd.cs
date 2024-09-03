namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.EssentialDocument;
/// <summary>
/// 删除纸质病例存储管理指令
/// </summary>
/// <param name="id"></param>
public record DeleteEssentialDocumentsCmd(string id) : IRequest<bool>;