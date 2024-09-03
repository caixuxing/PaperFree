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
    internal class FindMedicalRecordStatsHandlers : IRequestHandler<FindMedicalRecordStatsQry, MedicalRecordStatsDto>
    {
        private IOutpatientInfoRepo _outpatientInfoRepo;

        public FindMedicalRecordStatsHandlers(IOutpatientInfoRepo outpatientInfoRepo)
        {
            _outpatientInfoRepo = outpatientInfoRepo;
        }
        public async Task<MedicalRecordStatsDto> Handle(FindMedicalRecordStatsQry request, CancellationToken cancellationToken)
        {

            var query = _outpatientInfoRepo.QueryAll().AsNoTracking()
                        .WhereIf(x => x.OutDate.Year >= request.Year, request.Year != 0 );
            var groupedData = await query.GroupBy(s => new { s.OutDate.Month, s.Status })
                       .Select(group => new { status = group.Key, Count = group.Count(), group.Key.Month })
                       .ToDictionaryAsync(x => x.status, x => x.Count);
            var OutNumber = query.Count();
       return     new MedicalRecordStatsDto
            {
                submits = groupedData.Where(x => x.Key.Status == WorkFlowState.ALREADYCOMMIT).Select(x => new Submit
                {
                    SubmitMonth = x.Key.Month.ToString(),
                    SubmitNumber = x.Value,
                }).OrderBy(x => x.SubmitMonth).ToList(),
                archives = groupedData.Where(x => x.Key.Status == WorkFlowState.ALREADYARCHIVE).Select(x => new Archive
                {
                    ArchiveMonth = x.Key.Month.ToString(),
                    ArchiveNumber = x.Value,
                }).OrderBy(x => x.ArchiveMonth).ToList(),

            };
            }


    }
}
