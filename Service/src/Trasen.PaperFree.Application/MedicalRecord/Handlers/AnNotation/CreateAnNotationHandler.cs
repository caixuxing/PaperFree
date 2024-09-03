using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.MedicalRecord.Commands.AnNotation;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.AnNotation
{
    internal class CreateAnNotationHandler : IRequestHandler<CreateAnnotationTableCmd, string>
    {
        private readonly IAnnotationTableRepo annotationTableRepo;
        private readonly Validate<CreateAnnotationTableCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;
        private readonly IOutpatientInfoRepo outpatientInfoRepo;

        public CreateAnNotationHandler(IAnnotationTableRepo annotationTableRepo, Validate<CreateAnnotationTableCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            this.annotationTableRepo = annotationTableRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateAnnotationTableCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var outpatientInfo = await outpatientInfoRepo.QueryAll().SingleOrDefaultAsync(x => x.ArchiveId == request.Archiveid);
            if (outpatientInfo is null)
                throw new BusinessException(MessageType.Warn, "非法病历信息,操作失败！");
            var AnNotationInfo = annotationTableRepo.QueryAll().AsNoTracking().SingleOrDefaultAsync(x => x.Archiveid == request.Archiveid && x.FileId == request.FileId && x.CreatorId == request.CreatorId);
            if (AnNotationInfo is not null)
                throw new BusinessException(MessageType.Warn, "该用户已经存在批注信息！");
            var entity = new AnNotAtionTable(request.Archiveid, request.FileId, request.AnNotAtIOn, request.Deptcode, request.Lower, request.AnNotAtIOnDate, request.Remark, request.OrgCode, request.HospCode);
            await annotationTableRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}