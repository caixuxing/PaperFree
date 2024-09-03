using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers;

internal sealed class FindOrgDeptMemberPageListQueryHander : IRequestHandler<FindOrgDeptMemberPageListQuery, List<FindOrgDeptMemberPageListDto>>
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ICurrentUser currentUser;

    public FindOrgDeptMemberPageListQueryHander(IHttpClientFactory httpClientFactory,
        ICurrentUser currentUser)
    {
        this.httpClientFactory = httpClientFactory;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// 处理查找单个人员与科室关系
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<FindOrgDeptMemberPageListDto>> Handle(FindOrgDeptMemberPageListQuery request, CancellationToken cancellationToken)
    {
        return await httpClientFactory.PostAsync<List<FindOrgDeptMemberPageListDto>>(TrasenApiConst.OrgDeptMemberPageList, new
        {
            pageIndex = 1,
            pageSize = 9999,
            ORG_CODE = currentUser.OrgCode,
            HOSP_CODE = currentUser.HospCode,
            IS_DELETE = (char)YesOrNoType.NO
        }) ?? new();
    }
}