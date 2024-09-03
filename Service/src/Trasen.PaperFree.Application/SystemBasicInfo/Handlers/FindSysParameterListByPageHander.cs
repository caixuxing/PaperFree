using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers;

/// <summary>
///
/// </summary>
public class FindSysParameterListByPageHander : IRequestHandler<FindSysParameterListByPageQuery, List<FindSysParameterListByPageDto>>
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ICurrentUser currentUser;

    /// <summary>
    ///
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="currentUser"></param>
    public FindSysParameterListByPageHander(IHttpClientFactory httpClientFactory,
        ICurrentUser currentUser)
    {
        this.httpClientFactory = httpClientFactory;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// 处理查询系统参数指令
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<List<FindSysParameterListByPageDto>> Handle(FindSysParameterListByPageQuery request, CancellationToken cancellationToken)
    {
        return await httpClientFactory.PostAsync<List<FindSysParameterListByPageDto>>(TrasenApiConst.SysParameterList, new
        {
            pageIndex = 1,
            pageSize = 9999,
            ORG_CODE = currentUser.OrgCode,
            HOSP_CODE = currentUser.HospCode,
            IS_DELETE = (char)YesOrNoType.NO
        }) ?? new();
    }
}