using Trasen.PaperFree.Application.MedicalRecord.Commands.AnNotation;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.AnNotation
{
    internal class DeleteAnNotationHandler : IRequestHandler<DeleteAnNotationTableCmd, bool>
    {
        private readonly IAnnotationTableRepo annotationTableRepo;
        private readonly IUnitOfWork unitOfWork;

        public DeleteAnNotationHandler(IAnnotationTableRepo annotationTableRepo, IUnitOfWork unitOfWork)
        {
            this.annotationTableRepo = annotationTableRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAnNotationTableCmd request, CancellationToken cancellationToken)
        {
            var entity = await annotationTableRepo.FindId(request.id);
            if (entity is null) throw new BusinessException(MessageType.Error, "删除失败!", "批注实体不存在无法执行删除操作！");
            entity.ChangeDelete();
            annotationTableRepo.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}