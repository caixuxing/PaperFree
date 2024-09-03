using Trasen.PaperFree.Application.SystemBasicInfo.Dto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query;

/// <summary>
/// 查找机构参数指令
/// </summary>
public record FindOrgParameterListByPageQuery : IRequest<List<FindOrgParameterListByPageDto>>;

/// <summary>
/// 查找机构指令
/// </summary>
public record FindOrgQuery : IRequest<List<FindOrgDto>>;