using Trasen.PaperFree.Application.SystemBasicInfo.Commands.CaseManagement;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.CaseManagement
{
    internal class ModifyCaseShelfManagementHander : IRequestHandler<ModifyCaseShelfManagementCmd, bool>
    {
        private readonly ICaseShelfManagementRepo caseshelfmanagementrepo;
        private readonly Validate<ModifyCaseShelfManagementCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public ModifyCaseShelfManagementHander(
            ICaseShelfManagementRepo caseshelfmanagementrepo,
            Validate<ModifyCaseShelfManagementCmd> validate,
            IUnitOfWork unitOfWork, ICurrentUser curretUser)
        {
            this.caseshelfmanagementrepo = caseshelfmanagementrepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = curretUser;
        }

        public async Task<bool> Handle(ModifyCaseShelfManagementCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = await caseshelfmanagementrepo.FindById(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "更新失败!", "纸质病例存储管理实体不存在!");
            entity.UpadteCaseShelfManagement(request.WarehouseNumber, request.WarehouseName, request.ShelfNo, request.StorageNumberSegment, request.LineNumber,
                                   request.NumberOlumns, request.Status, request.OrgCode, request.HospCode, request.InputCode);
            caseshelfmanagementrepo.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}