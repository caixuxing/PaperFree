namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.PayConfig;
public record DeletePayConfigCmd(string id) : IRequest<bool>;