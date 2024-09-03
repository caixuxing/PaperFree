using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ArchiverMeumCmd;
using Trasen.PaperFree.Domain.ArchiveRecord.Entity;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ArchiverMeumHandler
{
    internal class CreateArchiverMeumHandler : IRequestHandler<CreateArchiverMeumCmd, string>
    {
        private readonly IArchiverMeumRepo iarchiverMeumRepo;
        private readonly Validate<CreateArchiverMeumCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;
        private readonly IGuidGenerator _guidGenerator;

        public CreateArchiverMeumHandler(IArchiverMeumRepo archiverMeumRepo,
       Validate<CreateArchiverMeumCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser, IGuidGenerator guidGenerator)
        {
            this.iarchiverMeumRepo = archiverMeumRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
            _guidGenerator = guidGenerator;
        }

        public async Task<string> Handle(CreateArchiverMeumCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            string id = _guidGenerator.Create().ToString();
            var entity = new ArchiverMeum(id, request.MenuName, request.ParentId, request.Permission,
                request.MeumType, request.Orderby, request.SecretLevel, request.IsAllorg, request.IsHighShots, request.IsSignature, request.OrgCode, request.HospCode);
            await iarchiverMeumRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}