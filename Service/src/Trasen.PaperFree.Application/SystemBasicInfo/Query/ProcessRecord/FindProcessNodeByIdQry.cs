using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;

/// <summary>
/// 按节点ID查询节点信息
/// </summary>
/// <param name="Id"></param>
public record FindProcessNodeByIdQry(string Id) : IRequest<ProcessNodeDetailDto?>;