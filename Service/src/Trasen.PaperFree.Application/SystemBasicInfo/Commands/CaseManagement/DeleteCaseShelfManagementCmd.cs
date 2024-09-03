namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.CaseManagement;
/// <summary>
/// 删除纸质病例存储管理指令
/// </summary>
/// <param name="id"></param>
public record DeleteCaseShelfManagementCmd(string id) : IRequest<bool>;