using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers;

internal sealed class FindSystemMenuByCurrentUserQueryHander : IRequestHandler<FindSystemMenuByCurrentUserQuery, List<FindSystemMenuByCurrentUserDto>>
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ICurrentUser currentUser;

    public FindSystemMenuByCurrentUserQueryHander(IHttpClientFactory httpClientFactory,
        ICurrentUser currentUser)
    {
        this.httpClientFactory = httpClientFactory;
        this.currentUser = currentUser;
    }

    public async Task<List<FindSystemMenuByCurrentUserDto>> Handle(FindSystemMenuByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        return await httpClientFactory.PostAsync<List<FindSystemMenuByCurrentUserDto>>(TrasenApiConst.UserMenu, new
        {
            ORG_CODE = currentUser.OrgCode,
            MEMBER_CODE = currentUser.UserName,
            HOSP_CODE = currentUser.HospCode
        }) ?? new();
    }
}