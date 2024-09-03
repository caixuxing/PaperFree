using Trasen.PaperFree.Application.SystemBasicInfo.Commands.PayConfig;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.PayConfigHandler
{
    internal sealed class CreatePayConfigHandler : IRequestHandler<CreatePayConfigCmd, string>
    {
        private readonly IPayConfigRepo ipayConfigRepo;
        private readonly Validate<CreatePayConfigCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;
        private readonly IGuidGenerator _guidGenerator;

        public CreatePayConfigHandler(IPayConfigRepo payConfigRepo,
            Validate<CreatePayConfigCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser, IGuidGenerator guidGenerator)
        {
            this.ipayConfigRepo = payConfigRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
            this._guidGenerator = guidGenerator;
        }

        public async Task<string> Handle(CreatePayConfigCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            string id = _guidGenerator.Create().ToString();
            var entity = new PayConfig(id, request.ServiceProviders, request.AppId, request.AppSecret, request.MerchantNumber, request.CallbackUrl, request.InterfaceVersion, request.Token,
                                       request.EncryptionKey, request.Completionnotification, request.IsEnable, request.OrgCode, request.HospCode, request.InputCode);
            await ipayConfigRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}