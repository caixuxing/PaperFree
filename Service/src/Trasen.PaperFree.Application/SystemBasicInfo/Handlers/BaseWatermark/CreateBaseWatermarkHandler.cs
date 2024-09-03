using Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseWatermark;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

using tecc = Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseWatermark
{
    internal class CreateBaseWatermarkHandler : IRequestHandler<CreateBaseWatermarkCmd, string>
    {
        private readonly IBaseWatermarkRepo ibaseWatermarkRepo;
        private readonly Validate<CreateBaseWatermarkCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;
        private readonly IGuidGenerator _guidGenerator;

        public CreateBaseWatermarkHandler(IBaseWatermarkRepo baseWatermarkRepo,
       Validate<CreateBaseWatermarkCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser, IGuidGenerator guidGenerator)
        {
            this.ibaseWatermarkRepo = baseWatermarkRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
            _guidGenerator = guidGenerator;
        }

        public async Task<string> Handle(CreateBaseWatermarkCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            string id = _guidGenerator.Create().ToString();
            var entity = new tecc.BaseWatermark(id, request.WatermarkName, request.UseScene, request.Color, request.Xstation, request.Ystation,
        request.Angle, request.Direction, request.Font,request.FontSize, request.Pellucidity, request.Hight, request.Width, request.Picture, request.PicX, request.PicY, "0",request.GapX,request.GapY, request.OrgCode, request.HospCode, request.InputCode);
            await ibaseWatermarkRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}