using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.SysOperLog;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.SysOperLog;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.SysOperLog
{
    internal class FindSysOperLogPageListHandler : IRequestHandler<FindSysOperLogPageListQry, PageData<List<SysOperLogPageDto>>>
    {
        private readonly ISysOperLogRepo sysOperLogRepo;
        public FindSysOperLogPageListHandler(ISysOperLogRepo sysOperLogRepo)
        {
            this.sysOperLogRepo = sysOperLogRepo;
        }

        public async Task<PageData<List<SysOperLogPageDto>>> Handle(FindSysOperLogPageListQry request, CancellationToken cancellationToken)
        {
            var query = sysOperLogRepo.QueryAll().AsNoTracking()
                .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
                .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
                .WhereIf(x => EF.Functions.Like(x.Title!, $"%{request.Title}%"), !string.IsNullOrWhiteSpace(request.Title))
                .WhereIf(x => EF.Functions.Like(x.OperName!, $"%{request.OperName}%"), !string.IsNullOrWhiteSpace(request.OperName))
                .WhereIf(x => x.BusinessType == request.BusinessType, !(request.BusinessType is null))
                .WhereIf(x => x.OperatorType == request.OperatorType, !(request.OperatorType is null))
                .WhereIf(x => x.Status == request.Status, !(request.Status is null))
                .OrderBy(x => x.CreationTime)
                .Select(x => new SysOperLogPageDto
                {
                    Id = x.Id,
                    BusinessTypeName =x.BusinessType==null?string.Empty:x.BusinessType.ToDescription(),
                    Elapsed = x.Elapsed,
                    OperatorTypeName = x.OperatorType.ToDescription(),
                    OperName = x.OperName,
                    OperTime = x.OperTime,
                    RequestType = x.RequestType,
                    RequestUrl = x.RequestUrl,
                    StatusName = x.Status.ToDescription(),
                    Title = x.Title
                });
            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}