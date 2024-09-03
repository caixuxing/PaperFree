using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.PayConfigDto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.PayConfigQuery;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.PayConfigHandler
{
    internal class FindPayConfigPageListHandler : IRequestHandler<FindPayConfigPageListQry, PageData<List<FindPagConfigPageListDto>?>>
    {
        private readonly IPayConfigRepo _repo;

        public FindPayConfigPageListHandler(IPayConfigRepo payConfigRepo)
        {
            _repo = payConfigRepo;
        }

        public async Task<PageData<List<FindPagConfigPageListDto>?>> Handle(FindPayConfigPageListQry request, CancellationToken cancellationToken)
        {
            var query = _repo.QueryAll().Select(x => new FindPagConfigPageListDto
            {
                Id = x.Id,
                ServiceProviders = x.ServiceProviders,
                AppId = x.AppId,
                AppSecret = x.AppSecret,
                MerchantNumber = x.MerchantNumber,
                CallbackUrl = x.CallbackUrl,
                InterfaceVersion = x.InterfaceVersion,
                Token = x.Token,
                EncryptionKey = x.EncryptionKey,
                OrgCode = x.OrgCode,
                HospCode = x.HospCode,
                InputCode = x.InputCode,
            }).WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
              .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
              .WhereIf(x => x.InputCode == request.InputCode, !string.IsNullOrWhiteSpace(request.InputCode))
              .WhereIf(x => x.ServiceProviders == request.ServiceProviders, !string.IsNullOrWhiteSpace(request.ServiceProviders)).AsNoTracking();

            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}