using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;

/// <summary>
/// 流程节点分页查询列表
/// </summary>
public record FindProcessNodePageListQry : IRequest<PageData<List<ProcessNodePageListDto>>>
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required]
    public string ProcessDesignId { get; set; } = string.Empty;

    /// <summary>
    ///
    /// </summary>
    [Required]
    public int PageIndex { get; private set; }
    /// <summary>
    ///
    /// </summary>
    [Required]
    public int PageSize { get; private set; }

    /// <summary>
    /// 设置分页参数
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public FindProcessNodePageListQry SetPagePram(int pageIndex, int pageSize)
    {
        this.PageIndex = pageIndex;
        this.PageSize = pageSize;
        return this;
    }
}