namespace Trasen.PaperFree.Domain.Shared.Entity.Interfances;

/// <summary>
/// 实体更新字段
/// </summary>
public interface IHasModified<T>
{
    /// <summary>
    /// 更新时间
    /// </summary>
    DateTime? LastModifyTime { get; set; }

    /// <summary>
    /// 更新者标识
    /// </summary>
    T LastModifyId { get; set; }
}