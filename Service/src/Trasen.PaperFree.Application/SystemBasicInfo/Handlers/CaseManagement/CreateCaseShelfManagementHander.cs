using Trasen.PaperFree.Application.SystemBasicInfo.Commands.CaseManagement;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.CaseManagement
{
    internal class CreateCaseShelfManagementHandler : IRequestHandler<CreateCaseShelfManagementCmd, string>
    {
        private readonly ICaseShelfManagementRepo caseshelfmanagementrepo;
        private readonly Validate<CreateCaseShelfManagementCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public CreateCaseShelfManagementHandler(ICaseShelfManagementRepo processDesignRepo,
       Validate<CreateCaseShelfManagementCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            this.caseshelfmanagementrepo = processDesignRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateCaseShelfManagementCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = new CaseShelfManagement(request.WarehouseNumber, request.WarehouseName, request.ShelfNo, request.StorageNumberSegment, request.LineNumber, request.NumberOlumns, "0",

                request.OrgCode, request.HospCode, request.InputCode);
            await caseshelfmanagementrepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}