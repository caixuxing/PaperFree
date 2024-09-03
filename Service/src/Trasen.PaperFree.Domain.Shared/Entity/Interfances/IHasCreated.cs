namespace Trasen.PaperFree.Domain.Shared.Entity.Interfances;

/// <summary>
/// 实体创建字段
/// </summary>
public interface IHasCreated<T> : ISoftDelete
{
    public T Id { get; set; }

    /// <summary>
    /// 创建人id
    /// </summary>
    T CreatorId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreationTime { get; set; }
}