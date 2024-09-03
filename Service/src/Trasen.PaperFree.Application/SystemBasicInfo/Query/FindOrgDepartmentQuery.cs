using Trasen.PaperFree.Application.SystemBasicInfo.Dto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query;

/// <summary>
/// 查找科室指令
/// </summary>
public record FindOrgDepartmentQuery : IRequest<List<FindOrgDepartmentDto>>;