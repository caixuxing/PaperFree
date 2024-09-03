using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.PayConfigDto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.PayConfigQuery;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.PayConfigHandler
{
    internal sealed class FindPayConfigHandler : IRequestHandler<FindPayConfigByIdQry, PayConfigDto?>
    {
        private readonly IPayConfigRepo ipayConfigRepo;

        public FindPayConfigHandler(IPayConfigRepo iPayConfigRepo)
        {
            this.ipayConfigRepo = iPayConfigRepo;
        }

        public async Task<PayConfigDto?> Handle(FindPayConfigByIdQry request, CancellationToken cancellationToken)
        {
            return await ipayConfigRepo.QueryAll().AsNoTracking()
                .Select(x => new PayConfigDto()
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
                    Completionnotification = x.Completionnotification,
                    IsEnable = x.IsEnable,
                }).FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}