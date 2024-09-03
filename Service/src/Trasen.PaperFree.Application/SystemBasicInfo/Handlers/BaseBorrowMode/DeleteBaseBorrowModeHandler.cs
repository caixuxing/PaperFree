using Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseBorrowMode;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseBorrowMode
{
    //
    internal class DeleteBaseBorrowModeHandler : IRequestHandler<DeleteBaseBorrowModeCmd, bool>
    {
        private readonly IBaseBorrowModeRepo baseborrowmoderepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteBaseBorrowModeHandler(
            IBaseBorrowModeRepo borrowmodeRepo,
            IUnitOfWork unitOfWork)
        {
            this.baseborrowmoderepo = borrowmodeRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBaseBorrowModeCmd request, CancellationToken cancellationToken)
        {
            var entity = await baseborrowmoderepo.FindById(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败！", "模板数据不存在无法执行删除操作！");
            entity.ChangeDelete();
            baseborrowmoderepo.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}