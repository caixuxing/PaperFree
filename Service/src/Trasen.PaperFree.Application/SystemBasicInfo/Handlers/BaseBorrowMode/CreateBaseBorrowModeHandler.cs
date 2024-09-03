using Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseBorrowMode;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseBorrowMode
{
    internal class CreateBaseBorrowModeHandler : IRequestHandler<CreateBaseBorrowModeCmd, string>
    {
        private readonly IBaseBorrowModeRepo ibaseborrowmoderepo;
        private readonly Validate<CreateBaseBorrowModeCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public CreateBaseBorrowModeHandler(IBaseBorrowModeRepo baseborrowmoderepo,
       Validate<CreateBaseBorrowModeCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            this.ibaseborrowmoderepo = baseborrowmoderepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateBaseBorrowModeCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = new Domain.SystemBasicData.Entity.BaseBorrowMode(request.ModeName, request.DeptCode, request.UserCode, request.IsEnable, "OrgCode", "HospCode", "inputCode");
            await ibaseborrowmoderepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}