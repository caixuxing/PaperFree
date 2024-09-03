using Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseBorrowMode;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseBorrowMode
{
    internal class ModifyBaseBorrowModeHandler : IRequestHandler<ModifyBaseBorrowModeCmd, bool>
    {
        private readonly IBaseBorrowModeRepo RepoBaseBorrowMode;
        private readonly Validate<ModifyBaseBorrowModeCmd> validate;
        private readonly IUnitOfWork unitOfWork;

        public ModifyBaseBorrowModeHandler(
            IBaseBorrowModeRepo BorrowModeRepo,
            Validate<ModifyBaseBorrowModeCmd> validate,
            IUnitOfWork unitOfWork)
        {
            this.RepoBaseBorrowMode = BorrowModeRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifyBaseBorrowModeCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = await RepoBaseBorrowMode.FindById(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "更新失败!", "纸质病例存储管理实体不存在!");
            entity.ChangeBaseBorrowMode(request.ModeName, request.DeptCode, request.UserCode, request.IsEnable);
            RepoBaseBorrowMode.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}