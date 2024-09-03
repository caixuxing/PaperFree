using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers;

internal sealed class FindPersonalParameterListByPageHander : IRequestHandler<FindPersonalParameterListByPageQuery, List<FindPersonalParameterListByPageDto>>
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ICurrentUser currentUser;

    public FindPersonalParameterListByPageHander(IHttpClientFactory httpClientFactory,
        ICurrentUser currentUser)
    {
        this.httpClientFactory = httpClientFactory;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// 处理查找个人参数指令
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<List<FindPersonalParameterListByPageDto>> Handle(FindPersonalParameterListByPageQuery request, CancellationToken cancellationToken)
    {
        return await httpClientFactory.PostAsync<List<FindPersonalParameterListByPageDto>>(TrasenApiConst.PersonalParameterList, new
        {
            pageIndex = 1,
            pageSize = 9999,
            ORG_CODE = currentUser.OrgCode,
            HOSP_CODE = currentUser.HospCode,
            IS_DELETE = (char)YesOrNoType.NO
        }) ?? new();
    }
}