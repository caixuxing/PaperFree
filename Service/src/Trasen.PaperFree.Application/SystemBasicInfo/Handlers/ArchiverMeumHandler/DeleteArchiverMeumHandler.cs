using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ArchiverMeumCmd;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ArchiverMeumHandler
{
    internal class DeleteArchiverMeumHandler : IRequestHandler<DeleteArchiverMeumCmd, bool>
    {
        private readonly IArchiverMeumRepo iarchiverMeumRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteArchiverMeumHandler(
            IArchiverMeumRepo archiverMeumRepo,
            IUnitOfWork unitOfWork)
        {
            this.iarchiverMeumRepo = archiverMeumRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteArchiverMeumCmd request, CancellationToken cancellationToken)
        {
            var entity = await iarchiverMeumRepo.FindById(request.id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败！", "当前删除信息不存在无法执行删除操作！");
            entity.ChangeDelete();
            iarchiverMeumRepo.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}