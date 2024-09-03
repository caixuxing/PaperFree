using Trasen.PaperFree.Application.SystemBasicInfo.Dto.SysOperLog;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.SysOperLog;

/// <summary>
/// 系统日志详细Cmd
/// </summary>
/// <param name="id"></param>
public record FindSysOperLogDetailByIdQry(string id) : IRequest<SysOperLogDetailDto?>;