using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseWatermark;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.BaseWatermark;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseWatermark
{
    internal class FindBaseWatermarkHandler : IRequestHandler<FindBaseWatermarkByIdQry, BaseWatermarkDto?>
    {
        private readonly IBaseWatermarkRepo ibaseWatermarkRepo;

        public FindBaseWatermarkHandler(IBaseWatermarkRepo baseWatermarkRepo)
        {
            this.ibaseWatermarkRepo = baseWatermarkRepo;
        }

        /// <summary>
        /// 返回值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BaseWatermarkDto?> Handle(FindBaseWatermarkByIdQry request, CancellationToken cancellationToken)
        {
            return await ibaseWatermarkRepo.QueryAll().AsNoTracking()
                .Select(_ => new BaseWatermarkDto()
                {
                    Id = _.Id,
                    WatermarkName = _.WatermarkName,
                    OrgCode = _.OrgCode,
                    UseScene = _.UseScene,
                    Color = _.Color,
                    Xstation = _.Xstation,
                    Ystation = _.Ystation,
                    Angle = _.Angle,
                    Direction = _.Direction,
                    Font = _.Font,
                    FontSize = _.FontSize,
                    Pellucidity = _.Pellucidity,
                    Hight = _.Hight,
                    Width = _.Width,
                    Picture = _.Picture,
                    PicX = _.PicX,
                    PicY = _.PicY,
                    Status = _.Status,
                    HospCode = _.HospCode,
                    InputCode = _.InputCode,
                    GapX = _.GapX,
                    GapY = _.GapY,
                }).FirstOrDefaultAsync(_ => _.Id == request.Id);
        }
    }
}