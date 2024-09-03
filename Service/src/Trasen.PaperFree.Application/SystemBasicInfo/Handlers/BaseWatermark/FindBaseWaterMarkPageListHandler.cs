using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseWatermark;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.BaseWatermark;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseWatermark
{
    internal class FindBaseWaterMarkPageListHandler : IRequestHandler<FindBaseWatermarkPageListQry, PageData<List<BaseWatermarkDto>?>>
    {
        private readonly IBaseWatermarkRepo _baseWatermarkRepo;

        public FindBaseWaterMarkPageListHandler(IBaseWatermarkRepo baseWatermarkRepo)
        {
            _baseWatermarkRepo = baseWatermarkRepo;
        }

        public async Task<PageData<List<BaseWatermarkDto>?>> Handle(FindBaseWatermarkPageListQry request, CancellationToken cancellationToken)
        {
            var query = _baseWatermarkRepo.QueryAll().Select(x => new BaseWatermarkDto
            {
                Id = x.Id,
                OrgCode = x.OrgCode,
                WatermarkName = x.WatermarkName,
                UseScene = x.UseScene,
                Color = x.Color,
                Xstation = x.Xstation,
                Ystation = x.Ystation,
                Angle = x.Angle,
                Direction = x.Direction,
                Font = x.Font,
                FontSize = x.FontSize,
                Pellucidity = x.Pellucidity,
                Hight = x.Hight,
                Width = x.Width,
                Picture = x.Picture,
                PicX = x.PicX,
                PicY = x.PicY,
                GapX = x.GapX,
                GapY = x.GapY,
                Status = x.Status,
                HospCode = x.HospCode,
                InputCode = x.InputCode,
            })
            .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
            .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
            .WhereIf(x => x.InputCode == request.InputCode, !string.IsNullOrWhiteSpace(request.InputCode))
            .WhereIf(x => x.WatermarkName == request.WatermarkName, !string.IsNullOrWhiteSpace(request.WatermarkName)).AsNoTracking();
            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);

            return new(total, request.PageSize, request.PageIndex, data);
        }
    }
}