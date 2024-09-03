namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseBorrowMode
{
    public record DeleteBaseBorrowModeCmd(string Id) : IRequest<bool>;
}