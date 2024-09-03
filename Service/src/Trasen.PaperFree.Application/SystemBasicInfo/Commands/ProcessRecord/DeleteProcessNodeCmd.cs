namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord
{
    /// <summary>
    /// 删除流程节点
    /// </summary>
    public record DeleteProcessNodeCmd(string id) : IRequest<bool>;
}