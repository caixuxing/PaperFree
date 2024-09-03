using Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseWatermark;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseWatermark
{
    internal class ModifyBaseWatermarkHandler : IRequestHandler<ModifyBaseWatermarkCmd, bool>
    {
        private readonly IBaseWatermarkRepo ibaseWatermarkRepo;
        private readonly Validate<ModifyBaseWatermarkCmd> validate;
        private readonly IUnitOfWork unitOfWork;

        public ModifyBaseWatermarkHandler(
            IBaseWatermarkRepo baseWatermarkRepo,
            Validate<ModifyBaseWatermarkCmd> validate,
            IUnitOfWork unitOfWork)
        {
            this.ibaseWatermarkRepo = baseWatermarkRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifyBaseWatermarkCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = await ibaseWatermarkRepo.FindById(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "更新失败!", "水印表实体不存在!");
            entity.ChangeBaseWatermark(request.Id, request.WatermarkName, request.UseScene, request.Color, request.Xstation, request.Ystation,
      request.Angle, request.Direction, request.Font,request.FontSize, request.Pellucidity, request.Hight, request.Width, request.Picture, request.PicX, request.PicY, request.Status, request.GapX,request.GapY,request.OrgCode, request.HospCode, request.InputCode);
            ibaseWatermarkRepo.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}