using Trasen.PaperFree.Application.SystemBasicInfo.Dto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query;

/// <summary>
/// 查找单个人员与科室关系指令
/// </summary>
public record FindOrgDeptMemberPageListQuery : IRequest<List<FindOrgDeptMemberPageListDto>>;