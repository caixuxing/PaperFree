using Trasen.PaperFree.Application.SystemBasicInfo.Commands.PayConfig;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.PayConfigHandler
{
    /// <summary>
    /// 支付配置表
    /// </summary>
    internal sealed class DeletePayConfigHandler : IRequestHandler<DeletePayConfigCmd, bool>
    {
        private readonly IPayConfigRepo ipayConfigRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeletePayConfigHandler(IPayConfigRepo payConfigRepo, IUnitOfWork unitOfWork)
        {
            this.ipayConfigRepo = payConfigRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePayConfigCmd request, CancellationToken cancellationToken)
        {
            var entity = await ipayConfigRepo.FindById(request.id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败！", "该条数据在支付配置表不存在无法执行删除操作！");
            entity.ChangeDelete();
            ipayConfigRepo.Update(entity);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}