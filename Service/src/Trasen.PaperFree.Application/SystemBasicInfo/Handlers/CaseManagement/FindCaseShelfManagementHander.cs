using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.CaseManagement;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.CaseManagement;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.CaseManagement
{
    internal sealed class FindCaseShelfManagementHander : IRequestHandler<FindCaseShelfManagementByIdQry, CaseShelfManagementDto?>
    {
        private readonly ICaseShelfManagementRepo caseshelfmanagementrepo;

        public FindCaseShelfManagementHander(ICaseShelfManagementRepo caseshelfmanagementrepo)
        {
            this.caseshelfmanagementrepo = caseshelfmanagementrepo;
        }

        public async Task<CaseShelfManagementDto?> Handle(FindCaseShelfManagementByIdQry request, CancellationToken cancellationToken)
        {
            return await caseshelfmanagementrepo.QueryAll().AsNoTracking()
                .Select(_ => new CaseShelfManagementDto()
                {
                    Id = _.Id,
                    WarehouseNumber = _.WarehouseNumber,
                    WarehouseName = _.WarehouseName,
                    ShelfNo = _.ShelfNo,
                    StorageNumberSegment = _.StorageNumberSegment,
                    LineNumber = _.LineNumber,
                    NumberOlumns = _.NumberOlumns,
                    Status = _.Status,
                    OrgCode = _.OrgCode,
                    HospCode = _.HospCode,
                    InputCode = _.InputCode,
                }).FirstOrDefaultAsync(_ => _.Id == request.Id);
        }
    }
}