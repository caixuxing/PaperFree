using Trasen.PaperFree.Application.SystemBasicInfo.Dto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query
{
    /// <summary>
    /// 查询系统参数指令
    /// </summary>
    public record FindSysParameterListByPageQuery : IRequest<List<FindSysParameterListByPageDto>>;
}