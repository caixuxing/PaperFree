using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal sealed class DeleteProcessDesignHandler : IRequestHandler<DeleteProcessDesignCmd, bool>
    {
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteProcessDesignHandler(
            IProcessDesignRepo processDesignRepo,
            IUnitOfWork unitOfWork)
        {
            this.processDesignRepo = processDesignRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProcessDesignCmd request, CancellationToken cancellationToken)
        {
            var entity = await processDesignRepo.FindById(request.id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败！", "节点信息不存在无法执行删除操作！");
            entity.ChangeDelete();
            entity.ProcessNodes.ToList().ForEach(item =>
            {
                item.ChangeDelete();
                item.NodeApprovers.ToList().ForEach(_ => _.ChangeDelete());
            });
            processDesignRepo.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}