using Trasen.PaperFree.Application.SystemBasicInfo.Commands.CaseManagement;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.CaseManagement
{
    internal sealed class DeleteCaseShelfManagementHander : IRequestHandler<DeleteCaseShelfManagementCmd, bool>
    {
        private readonly ICaseShelfManagementRepo ShelfManagementRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteCaseShelfManagementHander(
            ICaseShelfManagementRepo CaseShelfManagementRepo,
            IUnitOfWork unitOfWork)
        {
            this.ShelfManagementRepo = CaseShelfManagementRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCaseShelfManagementCmd request, CancellationToken cancellationToken)
        {
            var entity = await ShelfManagementRepo.FindById(request.id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败！", "节点信息不存在无法执行删除操作！");
            entity.ChangeDelete();
            ShelfManagementRepo.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}