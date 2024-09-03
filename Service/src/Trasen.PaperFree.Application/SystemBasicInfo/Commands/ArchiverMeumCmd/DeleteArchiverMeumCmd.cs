namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ArchiverMeumCmd;
public record DeleteArchiverMeumCmd(string id) : IRequest<bool>;