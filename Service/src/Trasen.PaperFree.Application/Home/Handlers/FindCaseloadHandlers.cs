using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.Home.Dto;
using Trasen.PaperFree.Application.Home.Query;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.Home.Handlers
{
    internal class FindCaseloadHandlers : IRequestHandler<FindCaseloadQry, CaseloadDto>
    {
        private IOutpatientInfoRepo _outpatientInfoRepo;

        public FindCaseloadHandlers(IOutpatientInfoRepo outpatientInfoRepo)
        {
            _outpatientInfoRepo = outpatientInfoRepo;
        }

        public async Task<CaseloadDto> Handle(FindCaseloadQry request, CancellationToken cancellationToken)
        {
            var query =  _outpatientInfoRepo.QueryAll().AsNoTracking()
                     .WhereIf(x => x.OutDate >= request.BeginDate && x.OutDate <= request.EndDate, request.BeginDate != null && request.EndDate != null);
            var groupedData = await query.GroupBy(s => s.Status)
                                .Select(group => new { status = group.Key, Count = group.Count() })
                                .ToDictionaryAsync(x => x.status, x => x.Count);
            int OutNumber = query.Count();
            return new CaseloadDto
            {
                TobesignedNumber = groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYAUDIT).Value,
                SignedNumber = groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYSIGN).Value,
                ReceiptedRate = Math.Round((double)groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYSIGN).Value / OutNumber, 2),

                ArchiveNumber = groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYARCHIVE).Value,
                TreatArchiveNumber = groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYCATALOG).Value,
                ArchivedRate =  Math.Round((double)groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYARCHIVE).Value / OutNumber, 2),

            };
        }
    }
}
