using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal class FindProcessDesignPageListHandler : IRequestHandler<FindProcessDesignPageListQry, PageData<List<ProcessDesignPageListDto>>>
    {
        private readonly IProcessDesignRepo _processDesignRepo;
        private readonly Validate<FindProcessDesignPageListQry> validate;
        private readonly IHttpClientFactory httpClientFactory;

        public FindProcessDesignPageListHandler(IProcessDesignRepo processDesignRepo,
            Validate<FindProcessDesignPageListQry> validate,
            IHttpClientFactory httpClientFactory)
        {
            _processDesignRepo = processDesignRepo;
            this.validate = validate;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<PageData<List<ProcessDesignPageListDto>>> Handle(FindProcessDesignPageListQry request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);

            var baseData = httpClientFactory.PostAsync<List<FindOrgDepartmentDto>>(TrasenApiConst.OrgDepartment, new
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

            var query = _processDesignRepo.QueryAll().AsNoTracking()
                  .Select(x => new ProcessDesignPageListDto
                  {
                      Id = x.Id,
                      ProcessName = x.ProcessName,
                      ProcessCode = x.ProcessCode,
                      DeptCode = x.DeptCode,
                      DeptName = string.Empty,
                      IsEnable = x.IsEnable,
                      processTempType = x.ProcessTempType,
                      HospCode = x.HospCode,
                      OrgCode = x.OrgCode,
                      CreteDateTime = x.CreationTime
                  })
                  .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
                  .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
                  .WhereIf(x => EF.Functions.Like(x.ProcessName, $"%{request.ProcessName}%"), !string.IsNullOrWhiteSpace(request.ProcessName))
                  .WhereIf(x => x.IsEnable == request.IsEnable, !(request.IsEnable is null));
            var total = await query.CountAsync(cancellationToken);
            var data = await query.OrderByDescending(x=>x.CreteDateTime).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            var baseDataDept = await baseData;
            var baseDataOrgResult = await baseDataOrg;
            data.ForEach(item =>
            {
                if (baseDataDept is not null &&
                    !string.IsNullOrWhiteSpace(item.DeptCode) &&
                    !string.IsNullOrWhiteSpace(item.OrgCode) &&
                    !string.IsNullOrWhiteSpace(item.HospCode))
                    item.DeptName = baseDataDept.FirstOrDefault(x =>
                    x.HOSP_CODE == item.HospCode &&
                    x.ORG_CODE == item.OrgCode &&
                    x.DEPT_ID == item.DeptCode)?.DEPT_NAME ?? string.Empty;
                if (baseDataOrgResult is not null && !string.IsNullOrWhiteSpace(item.OrgCode))
                {
                    item.OrgName = baseDataOrgResult.FirstOrDefault(x => x.ORG_CODE == item.OrgCode && x.ORG_TYPE == "0")?.ORG_NAME ?? string.Empty;
                    item.HospName = baseDataOrgResult.FirstOrDefault(x => x.ORG_CODE == item.HospCode && x.ORG_TYPE == "1")?.ORG_NAME ?? string.Empty;
                }
                item.processTempName = item.processTempType.ToDescription();
            });
            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}