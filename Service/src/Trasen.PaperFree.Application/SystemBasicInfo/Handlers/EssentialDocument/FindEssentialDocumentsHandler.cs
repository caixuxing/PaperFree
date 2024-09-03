using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.EssentialDocument;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.EssentialDocument;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.EssentialDocument
{
    /// <summary>
    /// 必传文件配置查询Handler
    /// </summary>
    internal class FindEssentialDocumentsHandler : IRequestHandler<FindEssentialDocumentByIdQry, EssentialDocumentsDto?>
    {
        private readonly IEssentialDocumentsRepo iessentialDocumentsRepo;

        public FindEssentialDocumentsHandler(IEssentialDocumentsRepo essentialDocumentsRepo)
        {
            this.iessentialDocumentsRepo = essentialDocumentsRepo;
        }

        public async Task<EssentialDocumentsDto?> Handle(FindEssentialDocumentByIdQry request, CancellationToken cancellationToken)
        {
            return await iessentialDocumentsRepo.QueryAll().AsNoTracking()
                .Select(x => new EssentialDocumentsDto()
                {
                    Id = x.Id,
                    DeptCode = x.DeptCode,
                    FatherMeumid = x.FatherMeumid,
                    MeumType = x.MeumType,
                    Status = x.Status,
                    OrgCode = x.OrgCode,
                    HospCode = x.HospCode,
                    InputCode = x.InputCode,
                }).FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}