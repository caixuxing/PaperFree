using Trasen.PaperFree.Application.SystemBasicInfo.Commands.PayConfig;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.PayConfigHandler
{
    internal class ModifyPayConfigHandler : IRequestHandler<ModifyPayConfigCmd, bool>
    {
        private readonly IPayConfigRepo _repo;
        private readonly Validate<ModifyPayConfigCmd> validate;
        private readonly IUnitOfWork _unitOfWork;

        public ModifyPayConfigHandler(
          IPayConfigRepo payConfigRepo,
          Validate<ModifyPayConfigCmd> validate,
          IUnitOfWork unitOfWork)
        {
            this._repo = payConfigRepo;
            this.validate = validate;
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifyPayConfigCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = await _repo.FindById(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "更新失败！", "支付配置表中当前数据不存在！");
            entity.UpdatePayConfig(request.EncryptionKey, request.AppId, request.AppSecret, request.MerchantNumber, request.CallbackUrl, request.InterfaceVersion, request.Token,
                                       request.EncryptionKey, request.Completionnotification, request.IsEnable,request.OrgCode,request.HospCode,request.InputCode);
            _repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}