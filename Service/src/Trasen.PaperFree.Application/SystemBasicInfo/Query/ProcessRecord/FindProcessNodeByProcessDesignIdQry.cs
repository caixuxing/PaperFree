using Trasen.PaperFree.Application.Dto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;

/// <summary>
/// 查询流程下的节点Qry
/// </summary>
public record FindProcessNodeByProcessDesignIdQry(string id) : IRequest<IEnumerable<DropSelectDto<string>>>;