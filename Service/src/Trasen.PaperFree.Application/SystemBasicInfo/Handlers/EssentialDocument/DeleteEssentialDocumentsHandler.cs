using Trasen.PaperFree.Application.SystemBasicInfo.Commands.EssentialDocument;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.EssentialDocument
{
    internal class DeleteEssentialDocumentsHandler : IRequestHandler<DeleteEssentialDocumentsCmd, bool>
    {
        private readonly IEssentialDocumentsRepo iessentialDocumentsRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteEssentialDocumentsHandler(
            IEssentialDocumentsRepo essentialDocumentsRepo,
            IUnitOfWork unitOfWork)
        {
            this.iessentialDocumentsRepo = essentialDocumentsRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteEssentialDocumentsCmd request, CancellationToken cancellationToken)
        {
            var entity = await iessentialDocumentsRepo.FindById(request.id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败！", "必传文件配置不存在无法执行删除操作！");
            entity.ChangeDelete();
            iessentialDocumentsRepo.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}