namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.BaseWatermark
{
    public record DeleteBaseWatermarkCmd(string id) : IRequest<bool>;
}