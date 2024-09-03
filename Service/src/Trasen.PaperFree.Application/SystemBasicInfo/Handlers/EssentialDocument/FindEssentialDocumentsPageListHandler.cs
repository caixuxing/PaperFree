using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.EssentialDocument;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.EssentialDocument;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.EssentialDocument
{
    internal class FindEssentialDocumentsPageListHandler : IRequestHandler<FindEssentialDocumentPageListQry, PageData<List<EssentialDocumentsPageListDto>?>>
    {
        private readonly IEssentialDocumentsRepo _essentialDocumentsRepo;

        public FindEssentialDocumentsPageListHandler(IEssentialDocumentsRepo essentialDocumentsRepo)
        {
            _essentialDocumentsRepo = essentialDocumentsRepo;
        }

        public async Task<PageData<List<EssentialDocumentsPageListDto>?>> Handle(FindEssentialDocumentPageListQry request, CancellationToken cancellationToken)
        {
            var query = _essentialDocumentsRepo.QueryAll().Select(x => new EssentialDocumentsPageListDto
            {
                Id = x.Id,
                DeptCode = x.DeptCode,
                FatherMeumid = x.FatherMeumid,
                MeumType = x.MeumType,
                Status = x.Status,
                OrgCode = x.OrgCode,
                HospCode = x.HospCode,
            }).WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
              .WhereIf(x => x.DeptCode == request.DeptCode, !string.IsNullOrWhiteSpace(request.DeptCode))
              .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode)).AsNoTracking();
            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}