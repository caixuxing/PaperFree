using Trasen.PaperFree.Application.MedicalRecord.Commands.AnNotation;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.AnNotation
{
    internal class ModifyAnNotationHandler : IRequestHandler<ModifyAnNotationTableCmd, bool>
    {
        private readonly IAnnotationTableRepo annotationTableRepo;
        private readonly Validate<ModifyAnNotationTableCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public ModifyAnNotationHandler(IAnnotationTableRepo annotationTableRepo, Validate<ModifyAnNotationTableCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            this.annotationTableRepo = annotationTableRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        public async Task<bool> Handle(ModifyAnNotationTableCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = await annotationTableRepo.FindId(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "更新失败!", "批注实体不存在!");
            entity.UpadteAnNotAtionTable(request.AnNotAtIOn, request.Lower, request.AnNotAtIOnDate, request.Remark);
            annotationTableRepo.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}