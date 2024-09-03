using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.MedicalRecord.Dto.AnNotation;
using Trasen.PaperFree.Application.MedicalRecord.Query.AnNotation;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.AnNotation
{
    internal sealed class FindAnNotationByIdHander : IRequestHandler<FindAnNotationByIdQry, AnNotationDto?>
    {
        private readonly IAnnotationTableRepo annotationTableRepo;

        public FindAnNotationByIdHander(IAnnotationTableRepo annotationTableRepo)
        {
            this.annotationTableRepo = annotationTableRepo;
        }

        public async Task<AnNotationDto?> Handle(FindAnNotationByIdQry request, CancellationToken cancellationToken)
        {
            return await annotationTableRepo.QueryAll().AsNoTracking()
                .Select(_ => new AnNotationDto()
                {
                    Id = _.Id,
                    Remark = _.Remark,
                    AnNotAtIOnDate = _.AnNotAtIOnDate,
                    Lower = _.Lower,
                    AnNotAtIOn = _.AnNotAtIOn,
                    Archiveid = _.Archiveid,
                    Deptcode = _.Deptcode,
                    FileId = _.FileId,
                    HospCode = _.HospCode,
                    OrgCode = _.OrgCode
                }).FirstOrDefaultAsync(_ => _.Id == request.Id);
        }
    }
}