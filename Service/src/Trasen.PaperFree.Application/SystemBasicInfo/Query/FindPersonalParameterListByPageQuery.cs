using Trasen.PaperFree.Application.SystemBasicInfo.Dto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query;

/// <summary>
/// 查询个人参数指令
/// </summary>
public record FindPersonalParameterListByPageQuery : IRequest<List<FindPersonalParameterListByPageDto>>;