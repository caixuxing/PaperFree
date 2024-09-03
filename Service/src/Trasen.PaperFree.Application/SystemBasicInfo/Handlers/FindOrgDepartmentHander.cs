using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers;

internal sealed class FindOrgDepartmentHander : IRequestHandler<FindOrgDepartmentQuery, List<FindOrgDepartmentDto>>
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ICurrentUser currentUser;

    public FindOrgDepartmentHander(IHttpClientFactory httpClientFactory,
        ICurrentUser currentUser)
    {
        this.httpClientFactory = httpClientFactory;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// 处理查找科室
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<List<FindOrgDepartmentDto>> Handle(FindOrgDepartmentQuery request, CancellationToken cancellationToken)
    {
        return await httpClientFactory.PostAsync<List<FindOrgDepartmentDto>>(TrasenApiConst.OrgDepartment, new
        {
            ORG_CODE = currentUser.OrgCode,
            HOSP_CODE = currentUser.HospCode,
            IS_DELETE = (char)YesOrNoType.NO,
            PageIndex = 1,
            PageSize = 9999
        }) ?? new();
    }
}