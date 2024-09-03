using Trasen.PaperFree.Application.SystemBasicInfo.Dto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query;

/// <summary>
/// 按当前用户查找系统菜单
/// </summary>
public record FindSystemMenuByCurrentUserQuery : IRequest<List<FindSystemMenuByCurrentUserDto>>;