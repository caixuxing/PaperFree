using Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseWatermark;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.BaseWatermark;
public record FindBaseWatermarkByIdQry(string Id) : IRequest<BaseWatermarkDto>;