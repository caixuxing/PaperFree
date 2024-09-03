using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo;
using Trasen.PaperFree.Application.MedicalRecord.Query.OutpatientInfo;
using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.OutpatientInfo
{
    internal class FindOutpatientInfoPageListHandler : IRequestHandler<FindOutpatientInfoPageListQry, PageData<List<OutpatientInfoDto>?>>
    {
        private readonly IOutpatientInfoRepo _repo;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="options"></param>
        public FindOutpatientInfoPageListHandler(
            IOutpatientInfoRepo repo,
            IHttpClientFactory httpClientFactory)
        {
            _repo = repo;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PageData<List<OutpatientInfoDto>?>> Handle(FindOutpatientInfoPageListQry request, CancellationToken cancellationToken)
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
            var query = _repo.QueryAll().Select(x => new
            {
                x.ArchiveId,
                x.Status,
                x.AdmissId,
                x.VisitId,
                x.Name,
                x.SexType,
                x.EnterDate,
                x.OutDate,
                x.OutDeptCode,
                x.DoctorZzysCode,
                x.IsOverdate,
                x.OrgCode,
                x.HospCode,
                x.DoctorZyysCode,
                x.ChargeNurseCode,
            }).AsNoTracking()
            .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
            .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
            .WhereIf(x => x.OutDeptCode == request.OutDeptCode, !string.IsNullOrWhiteSpace(request.OutDeptCode))
            .WhereIf(x => x.AdmissId == request.AdmissId, !string.IsNullOrWhiteSpace(request.AdmissId))
            .WhereIf(x => x.Name.Contains(request.Name), !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(x => x.Status == request.Status, request.Status != null)
            .WhereIf(x => x.IsOverdate == request.IsOverdate, !string.IsNullOrWhiteSpace(request.IsOverdate))
            .WhereIf(x => x.OutDate >= request.BeginOutDate && x.OutDate <= request.EndOutDate, request.BeginOutDate != null && request.EndOutDate != null)
            .Select(x => new OutpatientInfoDto
            {
                ArchiveId = x.ArchiveId,
                Status = x.Status,
                AdmissId = x.AdmissId,
                VisitId = x.VisitId,
                Name = x.Name,
                SexType = x.SexType,
                EnterDate = x.EnterDate,
                OutDate = x.OutDate,
                OutDeptCode = x.OutDeptCode,
                DoctorZzysCode = x.DoctorZzysCode,
                DoctorZyysCode = x.DoctorZyysCode,
                ChargeNurseCode = x.ChargeNurseCode,
                HospCode=x.HospCode,
                OrgCode=x.OrgCode,
            });

            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            var baseDataDept = await basedata;
            var baseDataMemberResult = await basedatamember;
            data.ToList().ForEach(item =>
            {
                item.StatusName = item.Status.ToDescription().ToString();
                item.SexName = item.SexType.ToDescription().ToString();
                item.Days = (item.OutDate - item.EnterDate).Days;
                if (baseDataDept is not null && !string.IsNullOrWhiteSpace(item.OutDeptCode))
                    item.OutDeptName = baseDataDept.FirstOrDefault(x => x.DEPT_ID == item.OutDeptCode)?.DEPT_NAME ?? string.Empty;
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