using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ArchiverMeumCmd;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ArchiverMeumHandler
{
    //internal sealed class 批量ModifyArchiverMeumCmdHandler : IRequestHandler<批量ModifyArchiverMeumCmd, bool>
    //{
    //    readonly IArchiverMeumRepo iarchiverMeumRepo;
    //    readonly Validate<ModifyArchiverMeumCmd> validate;
    //    readonly IUnitOfWork unitOfWork;
    //    public async Task<bool> Handle(批量ModifyArchiverMeumCmd request, CancellationToken cancellationToken)
    //    {
    //       var data= request.cmd.Select(x => new ArchiverMeum(x.FatherMeumid,x.));

    //       await iarchiverMeumRepo.AddAsyncList(data, cancellationToken);
    //        return true;
    //    }
    //}

    internal class ModifyArchiverMeumHandler : IRequestHandler<ModifyArchiverMeumCmd, bool>
    {
        private readonly IArchiverMeumRepo iarchiverMeumRepo;
        private readonly Validate<ModifyArchiverMeumCmd> validate;
        private readonly IUnitOfWork unitOfWork;

        public ModifyArchiverMeumHandler(
            IArchiverMeumRepo archiverMeumRepo,
            Validate<ModifyArchiverMeumCmd> validate,
            IUnitOfWork unitOfWork)
        {
            this.iarchiverMeumRepo = archiverMeumRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifyArchiverMeumCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = await iarchiverMeumRepo.FindById(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "更新失败!", "当前目录数据不存在!");
            entity.ChangeArchiverMeum(request.MenuName, request.ParentId, request.Permission, request.MeumType,
                                       request.Orderby, request.SecretLevel, request.IsHighShots, request.IsSignature, request.IsAllorg);
            iarchiverMeumRepo.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public Task<bool> Handle(List<ModifyArchiverMeumCmd> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}