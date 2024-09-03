using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo;
using Trasen.PaperFree.Application.MedicalRecord.Query.OutpatientInfo;
using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.OutpatientInfo
{
    internal class FindOutpatientEmergencyPageListHandler : IRequestHandler<FindOutpatientEmergencyPageListQry, PageData<List<OutpatientEmergencyDto>?>>
    {
        private readonly IOutpatientEmergencyRepo  outpatientEmergencyRepo;
        private readonly IHttpClientFactory _httpClientFactory;

        public FindOutpatientEmergencyPageListHandler(IOutpatientEmergencyRepo  outpatientEmergency, IHttpClientFactory httpClientFactory)
        {
            outpatientEmergencyRepo = outpatientEmergency;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<PageData<List<OutpatientEmergencyDto>?>> Handle(FindOutpatientEmergencyPageListQry request, CancellationToken cancellationToken)
        {
            var basedata = _httpClientFactory.PostAsync<List<FindOrgDepartmentDto>>(TrasenApiConst.OrgDepartment, new
            {
                pageIndex = 1,
                pageSize = 9999,
                ORG_CODE = request.OrgCode,
                IS_DELETE = (char)YesOrNoType.NO
            });
            var basedatamember = _httpClientFactory.PostAsync<List<FindOrgMemberDto>>(TrasenApiConst.OrgMemberPageList, new
            {
                pageIndex = 1,
                pageSize = 9999,
                ORG_CODE = request.OrgCode,
                IS_DELETE = (char)YesOrNoType.NO
            });
            var baseDataOrg = _httpClientFactory.PostAsync<List<FindOrgDto>>(TrasenApiConst.Org, new
            {
                ORG_CODE = request.OrgCode,
                IS_DELETE = "N",
                PageIndex = 1,
                PageSize = 9999
            });
            var query = outpatientEmergencyRepo.QueryAll().AsNoTracking().Select(x => new OutpatientEmergencyDto
            {
                HospRecordId=x.HospRecordId,
                OrgCode = x.OrgCode,
                HospCode = x.HospCode,
                Name = x.Name,
                SexType = x.SexType,
                DateOfBirth = x.DateOfBirth,
                Age = x.Age,
                SeePatientsDate = x.SeePatientsDate,
                SeeDeptCode = x.SeeDeptCode,
                ReceiveDoctorCode = x.ReceiveDoctorCode,
                IcdName = x.IcdName,
            })
                .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
                .WhereIf(x => x.HospRecordId == request.HospRecordId, !string.IsNullOrWhiteSpace(request.HospRecordId))
                .WhereIf(x => x.Name == request.Name, !string.IsNullOrWhiteSpace(request.Name))
                .WhereIf(x => x.SeeDeptCode == request.SeeDeptCode, !string.IsNullOrWhiteSpace(request.SeeDeptCode))
                .WhereIf(x => x.IcdName == request.IcdName, !string.IsNullOrWhiteSpace(request.IcdName));
            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            var baseDataDept = await basedata;
            var baseDataOrgResult = await baseDataOrg;
            var baseDataMemberResult = await basedatamember;
            data.ToList().ForEach(item =>
            {
                item.SexName = item.SexType.ToDescription().ToString();
                if (baseDataDept is not null && !string.IsNullOrWhiteSpace(item.SeeDeptCode) && !string.IsNullOrWhiteSpace(item.HospCode) && !string.IsNullOrWhiteSpace(item.OrgCode))
                    item.SeeDeptName = baseDataDept.FirstOrDefault(x => x.HOSP_CODE == item.HospCode && x.ORG_CODE == item.OrgCode && x.DEPT_ID == item.SeeDeptCode)?.DEPT_NAME ?? string.Empty;
                if (baseDataMemberResult is not null && !string.IsNullOrWhiteSpace(item.ReceiveDoctorCode))
                    item.ReceiveDoctorName = baseDataMemberResult.FirstOrDefault(x => x.MENBER_ID == item.ReceiveDoctorCode)?.NAME ?? string.Empty;
                if (baseDataOrgResult is not null && !string.IsNullOrWhiteSpace(item.OrgCode))
                {
                    item.OrgName = baseDataOrgResult.FirstOrDefault(x => x.ORG_CODE == item.OrgCode && x.ORG_TYPE == "0")?.ORG_NAME ?? string.Empty;
                    item.HospName = baseDataOrgResult.FirstOrDefault(x => x.ORG_CODE == item.HospCode && x.ORG_TYPE == "1")?.ORG_NAME ?? string.Empty;
                }
            });
            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}
