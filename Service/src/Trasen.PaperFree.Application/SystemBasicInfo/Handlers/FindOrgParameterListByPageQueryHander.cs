using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers;

internal sealed class FindOrgParameterListByPageQueryHander : IRequestHandler<FindOrgParameterListByPageQuery, List<FindOrgParameterListByPageDto>>
   , IRequestHandler<FindOrgQuery, List<FindOrgDto>>
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ICurrentUser currentUser;

    public FindOrgParameterListByPageQueryHander(IHttpClientFactory httpClientFactory,
        ICurrentUser currentUser)
    {
        this.httpClientFactory = httpClientFactory;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// 处理查找机构参数
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<List<FindOrgParameterListByPageDto>> Handle(FindOrgParameterListByPageQuery request, CancellationToken cancellationToken)
    {
        return await httpClientFactory.PostAsync<List<FindOrgParameterListByPageDto>>(TrasenApiConst.OrgParameterPageList, new
        {
            pageIndex = 1,
            pageSize = 9999,
            ORG_CODE = currentUser.OrgCode,
            HOSP_CODE = currentUser.HospCode,
            IS_DELETE = (char)YesOrNoType.NO
        }) ?? new();
    }

    /// <summary>
    /// 处理查找机构指令
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<List<FindOrgDto>> Handle(FindOrgQuery request, CancellationToken cancellationToken)
    {
        return await httpClientFactory.PostAsync<List<FindOrgDto>>(TrasenApiConst.Org, new
        {
            ORG_CODE = currentUser.OrgCode,
            IS_DELETE = "N",
            PageIndex = 1,
            PageSize = 9999
        }) ?? new();
    }
}