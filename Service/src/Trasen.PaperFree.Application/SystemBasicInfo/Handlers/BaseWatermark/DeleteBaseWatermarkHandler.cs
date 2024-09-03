using Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseWatermark;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseWatermark
{
    internal class DeleteBaseWatermarkHandler : IRequestHandler<DeleteBaseWatermarkCmd, bool>
    {
        private readonly IBaseWatermarkRepo ibaseWatermarkRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteBaseWatermarkHandler(
            IBaseWatermarkRepo baseWatermarkRepo,
            IUnitOfWork unitOfWork)
        {
            this.ibaseWatermarkRepo = baseWatermarkRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBaseWatermarkCmd request, CancellationToken cancellationToken)
        {
            var entity = await ibaseWatermarkRepo.FindById(request.id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败！", "当前删除信息不存在无法执行删除操作！");
            entity.ChangeDelete();
            ibaseWatermarkRepo.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}