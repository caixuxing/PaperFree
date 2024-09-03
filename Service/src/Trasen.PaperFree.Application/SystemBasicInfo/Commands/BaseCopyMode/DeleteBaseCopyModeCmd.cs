namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseCopyMode
{
    public record DeleteBaseCopyModeCmd(string id) : IRequest<bool>;
}