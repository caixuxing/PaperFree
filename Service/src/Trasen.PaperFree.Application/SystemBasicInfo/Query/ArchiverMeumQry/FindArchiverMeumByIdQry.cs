using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ArchiverMeumDto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.ArchiverMeumQry;
public record FindArchiverMeumByIdQry(string id) : IRequest<FindArchiverMeumDto>;