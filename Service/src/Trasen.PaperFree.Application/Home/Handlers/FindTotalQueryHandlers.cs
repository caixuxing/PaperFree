using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trasen.PaperFree.Application.Home.Dto;
using Trasen.PaperFree.Application.Home.Query;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.Home.Handlers
{
    internal class FindTotalQueryHandlers : IRequestHandler<FindTotalQueryQry, TotalQueryDto>
    {
        private IOutpatientInfoRepo _outpatientInfoRepo;

        public FindTotalQueryHandlers(IOutpatientInfoRepo outpatientInfoRepo)
        {
            _outpatientInfoRepo = outpatientInfoRepo;
        }

        public async Task<TotalQueryDto> Handle(FindTotalQueryQry request, CancellationToken cancellationToken)
        {
            var query = _outpatientInfoRepo.QueryAll().AsNoTracking()
                        .WhereIf(x => x.OutDate >= request.BeginDate && x.OutDate <= request.EndDate, request.BeginDate != null && request.EndDate != null);
            var groupedData = await query.GroupBy(s => s.Status)
                                .Select(group => new { status = group.Key, Count = group.Count() })
                                .ToDictionaryAsync(x => x.status, x => x.Count);
            var OutNumber = query.Count();
            return new TotalQueryDto() {
                OutNumber = OutNumber,
                TobesignedNumber=groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYAUDIT).Value,
                SignedNumber =  groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYSIGN).Value,
                ArchiveNumber = groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYARCHIVE).Value,
                RecallReviewedNumber = groupedData.FirstOrDefault(x => x.Key == WorkFlowState.RECALLAWAITAUDIT).Value,
                ArchivedNumber = groupedData.FirstOrDefault(x => x.Key == WorkFlowState.ALREADYCATALOG).Value,
            };
       
        }
    }

}
