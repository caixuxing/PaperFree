using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal sealed class DeleteProcessNodeHandler : IRequestHandler<DeleteProcessNodeCmd, bool>
    {
        private readonly IProcessNodeRepo processNodeRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteProcessNodeHandler(IProcessNodeRepo processNodeRepo, IUnitOfWork unitOfWork)
        {
            this.processNodeRepo = processNodeRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProcessNodeCmd request, CancellationToken cancellationToken)
        {
            var entity = await processNodeRepo.FindById(request.id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败!", "节点信息不存在无法执行删除!");
            entity.ChangeDelete();
            foreach (var item in entity.NodeApprovers)
            {
                item.ChangeDelete();
            }
            await processNodeRepo.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}