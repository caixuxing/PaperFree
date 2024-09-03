namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseCopyModedetail
{
    public record DeleteBaseCopyModedetailCmd(string id) : IRequest<bool>;
}