using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo;
using Trasen.PaperFree.Application.MedicalRecord.Query.OutpatientInfo;
using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Domain.Shared.Extend;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.OutpatientInfo
{
    internal class FindInpatientInfoPageListHandler : IRequestHandler<FindInpatientInfoPageListQry, PageData<List<InpatientInfoDto>>?>
    {
        private readonly IInpatientInfoRepo _inpatientInfoRepo;
        private readonly IHttpClientFactory _httpClientFactory;

        public FindInpatientInfoPageListHandler(IInpatientInfoRepo inpatientInfoRepo, IHttpClientFactory httpClientFactory)
        {
            _inpatientInfoRepo = inpatientInfoRepo;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PageData<List<InpatientInfoDto>>?> Handle(FindInpatientInfoPageListQry request, CancellationToken cancellationToken)
        {
            var basedata = _httpClientFactory.PostAsync<List<FindOrgDepartmentDto>>(TrasenApiConst.OrgDepartment, new
            {
                pageIndex = 1,
                pageSize = 9999,
                ORG_CODE = request.OrgCode,
                HOSP_CODE = request.HospCode,
                IS_DELETE = (char)YesOrNoType.NO
            });
            var basedatamember = _httpClientFactory.PostAsync<List<FindOrgMemberDto>>(TrasenApiConst.OrgMemberPageList, new
            {
                pageIndex = 1,
                pageSize = 9999,
                ORG_CODE = request.OrgCode,
                HOSP_CODE = request.HospCode,
                IS_DELETE = (char)YesOrNoType.NO
            });
            var query = _inpatientInfoRepo.QueryAll().Select(x => new
            {
                x.AdmissId,
                x.VisitId,
                x.Name,
                x.SexType,
                x.EnterDate,
                x.EnterDept,
                x.DoctorZzysCode,
                x.DoctorZyysCode,
                x.ChargeNurseCode,
                x.OrgCode,
                x.HospCode,
            }).AsNoTracking()
             .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
            .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
            .WhereIf(x => x.EnterDept == request.EnterDeptCode, !string.IsNullOrWhiteSpace(request.EnterDeptCode))
            .WhereIf(x => x.AdmissId == request.AdmissId, !string.IsNullOrWhiteSpace(request.AdmissId))
            .WhereIf(x => x.EnterDate >= request.BeginDate && x.EnterDate <= request.EnterDate, request.BeginDate != null && request.EnterDate != null)
            .Select(x => new InpatientInfoDto
            {
                Status = "在院",
                AdmissId = x.AdmissId,
                VisitId = x.VisitId,
                Name = x.Name,
                SexType = x.SexType,
                EnterDate = x.EnterDate,
                EnterDept = x.EnterDept,
                DoctorZzysCode = x.DoctorZzysCode,
                DoctorZyysCode = x.DoctorZyysCode,
                ChargeNurseCode = x.ChargeNurseCode,
                days = 0,
            });
            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            var baseDataDept = await basedata;
            var baseDataMemberResult = await basedatamember;
            data.ToList().ForEach(item =>
            {
                item.days = (DateTime.Now - item.EnterDate).Days;
                item.SexName = item.SexType.ToDescription().ToString();
                if (baseDataDept is not null && !string.IsNullOrWhiteSpace(item.EnterDept))
                    item.EnterDept = baseDataDept.FirstOrDefault(x => x.DEPT_ID == item.EnterDept)?.DEPT_NAME ?? string.Empty;
                if (baseDataMemberResult is not null && !string.IsNullOrWhiteSpace(item.DoctorZyysCode))
                    item.DoctorZyysCode = baseDataMemberResult.FirstOrDefault(x => x.MENBER_ID == item.DoctorZyysCode)?.NAME ?? string.Empty;
                if (baseDataMemberResult is not null && !string.IsNullOrWhiteSpace(item.DoctorZyysCode))
                    item.DoctorZzysCode = baseDataMemberResult.FirstOrDefault(x => x.ORG_CODE == item.DoctorZzysCode)?.NAME ?? string.Empty;
                if (baseDataMemberResult is not null && !string.IsNullOrWhiteSpace(item.ChargeNurseCode))
                    item.ChargeNurseCode = baseDataMemberResult.FirstOrDefault(x => x.MENBER_ID == item.ChargeNurseCode)?.NAME ?? string.Empty;
            });
            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}
