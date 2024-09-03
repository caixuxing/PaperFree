using Trasen.PaperFree.Application.SystemBasicInfo.Commands.EssentialDocument;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.EssentialDocument
{
    internal sealed class CreateEssentialDocumentsListHandler : IRequestHandler<CreateEssentialDocumentsListCmd, bool>
    {
        private readonly IEssentialDocumentsRepo iessentialDocumentsRepo;
        private readonly Validate<ModifyEssentialDocumentsCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGuidGenerator guidGenerator;

        public CreateEssentialDocumentsListHandler(IEssentialDocumentsRepo iessentialDocumentsRepo, Validate<ModifyEssentialDocumentsCmd> validate, IUnitOfWork unitOfWork, IGuidGenerator guidGenerator)
        {
            this.iessentialDocumentsRepo = iessentialDocumentsRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.guidGenerator = guidGenerator;
        }

        public async Task<bool> Handle(CreateEssentialDocumentsListCmd request, CancellationToken cancellationToken)
        {
            // var data = request.cmd.Select(x => new EssentialDocuments(guidGenerator.Create().ToString(),x.DeptCode, x.FatherMeumid, x.MeumType, "0", x.OrderId, x.OrgCode, x.HospCode, x.InputCode)).ToList();
            var data = request.cmd.ListTreeDate.Select(x => new EssentialDocuments(guidGenerator.Create().ToString(), request.cmd.DeptCode, x.FatherMeumid,
                request.cmd.MeumType, "0", request.cmd.OrderId, request.cmd.OrgCode, request.cmd.HospCode, request.cmd.InputCode)).ToList();
            await iessentialDocumentsRepo.AddAsyncList(data, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }

    internal class CreateEssentialDocumentsHandler : IRequestHandler<CreateEssentialDocumentsCmd, string>
    {
        private readonly IEssentialDocumentsRepo iessentialDocumentsRepo;
        private readonly Validate<CreateEssentialDocumentsCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public CreateEssentialDocumentsHandler(IEssentialDocumentsRepo essentialDocumentsRepo, Validate<CreateEssentialDocumentsCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            this.iessentialDocumentsRepo = essentialDocumentsRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateEssentialDocumentsCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = new EssentialDocuments(request.DeptCode, request.FatherMeumid, request.MeumType, "0", request.OrderId, currentUser.OrgCode, currentUser.HospCode, currentUser.InputCode);
            await iessentialDocumentsRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}