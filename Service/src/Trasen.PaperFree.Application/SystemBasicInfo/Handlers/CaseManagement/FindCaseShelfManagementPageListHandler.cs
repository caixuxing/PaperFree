using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.CaseManagement;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.CaseManagement;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.CaseManagement
{
    internal class FindCaseShelfManagementPageListHandler : IRequestHandler<FindCaseShelfManagementPageListQry, PageData<List<CaseShelfManagementPageListDto>>>
    {
        private readonly ICaseShelfManagementRepo _caseShelfManagementRepo;

        public FindCaseShelfManagementPageListHandler(ICaseShelfManagementRepo caseShelfManagementRepo)
        {
            _caseShelfManagementRepo = caseShelfManagementRepo;
        }

        public async Task<PageData<List<CaseShelfManagementPageListDto>>> Handle(FindCaseShelfManagementPageListQry request, CancellationToken cancellationToken)
        {
            var query = _caseShelfManagementRepo.QueryAll().Select(x => new CaseShelfManagementPageListDto
            {
                Id = x.Id,
                WarehouseNumber = x.WarehouseNumber,
                WarehouseName = x.WarehouseName,
                ShelfNo = x.ShelfNo,
                StorageNumberSegment = x.StorageNumberSegment,
                LineNumber = x.LineNumber,
                NumberOlumns = x.NumberOlumns,
                Status = x.Status,
                OrgCode = x.OrgCode,
                HospCode = x.HospCode,
                InputCode = x.InputCode,
            })
            .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
            .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
            .WhereIf(x => x.InputCode == request.InputCode, !string.IsNullOrWhiteSpace(request.InputCode)).AsNoTracking();
            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}