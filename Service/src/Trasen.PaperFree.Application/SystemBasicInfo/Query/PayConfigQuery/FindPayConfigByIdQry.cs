using Trasen.PaperFree.Application.SystemBasicInfo.Dto.PayConfigDto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.PayConfigQuery;
public record FindPayConfigByIdQry(string Id) : IRequest<PayConfigDto>;