using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.DeptMeunTree;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.DeptMeunTree;
using Trasen.PaperFree.Domain.BasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.DeptMeunTreeHandler
{
    internal class FindDeptMenuTreePageListHandler : IRequestHandler<FindDeptMenuTreePageListQuery, PageData<List<DeptMeunTreePageListDto>?>>
    {
        private readonly IDeptMenuTreeRepo _deptMeumTreeRepo;
        private readonly IHttpClientFactory httpClientFactory;

        public FindDeptMenuTreePageListHandler(IDeptMenuTreeRepo deptMeumTreeRepo, IHttpClientFactory httpClientFactory)
        {
            _deptMeumTreeRepo = deptMeumTreeRepo;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<PageData<List<DeptMeunTreePageListDto>?>> Handle(FindDeptMenuTreePageListQuery request, CancellationToken cancellationToken)
        {
            var basedata = httpClientFactory.PostAsync<List<FindOrgDepartmentDto>>(TrasenApiConst.OrgDepartment, new
            {
                pageIndex = 1,
                pageSize = 9999,
                ORG_CODE = request.OrgCode,
                HOSP_CODE = request.HospCode,
                IS_DELETE = (char)YesOrNoType.NO
            });
            var baseDataOrg = httpClientFactory.PostAsync<List<FindOrgDto>>(TrasenApiConst.Org, new
            {
                ORG_CODE = request.OrgCode,
                IS_DELETE = "N",
                PageIndex = 1,
                PageSize = 9999
            });
            var basedivision = httpClientFactory.PostAsync<List<BaseAdminDivision>>(TrasenApiConst.BasAdminDivisionList, new
            {

                IS_DELETE = "N",
                PageIndex = 1,
                PageSize = 9999
            });

            var query = _deptMeumTreeRepo.QueryAll().Select(x => new DeptMeunTreePageListDto
            {
                //Id = x.Id,
                DeptId = x.DeptId,
                OrgCode = x.OrgCode,
                HospCode = x.HospCode,
                InputCode = x.InputCode,
            }).Distinct().WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
              .WhereIf(x => x.DeptId == request.DeptCode, !string.IsNullOrWhiteSpace(request.DeptCode))
              .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
              .AsNoTracking();
            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            var baseDataDept = await basedata;
            var baseDataOrgResult = await baseDataOrg;
            var baseAdminResult = await basedivision;
            data.ForEach(item =>
            {
                if (baseDataDept is not null && !string.IsNullOrWhiteSpace(item.DeptId) && !string.IsNullOrWhiteSpace(item.HospCode) && !string.IsNullOrWhiteSpace(item.OrgCode))
                    item.DeptName = baseDataDept.FirstOrDefault(x => x.HOSP_CODE == item.HospCode && x.ORG_CODE == item.OrgCode && x.DEPT_ID == item.DeptId)?.DEPT_NAME ?? string.Empty;
                if (baseDataOrgResult is not null && !string.IsNullOrWhiteSpace(item.OrgCode))
                    item.OrgName = baseDataOrgResult.FirstOrDefault(x => x.ORG_CODE == item.OrgCode && x.ORG_TYPE == "0")?.ORG_NAME ?? string.Empty;
                if (baseDataOrgResult is not null && !string.IsNullOrWhiteSpace(item.HospCode))
                    item.HospName = baseDataOrgResult.FirstOrDefault(x => x.ORG_CODE == item.HospCode && x.ORG_TYPE == "1")?.ORG_NAME ?? string.Empty;
                if (baseAdminResult is not null && !string.IsNullOrWhiteSpace(item.InputCode))
                    item.InputName = baseAdminResult.FirstOrDefault(x => x.AREA_CODE == item.InputCode)?.AREA_NAME ?? string.Empty;
            });
            return new(total, request.PageSize, request.PageIndex, data);
        }

    }
}