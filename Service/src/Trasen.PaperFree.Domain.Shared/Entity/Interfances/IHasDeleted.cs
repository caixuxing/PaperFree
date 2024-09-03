namespace Trasen.PaperFree.Domain.Shared.Entity.Interfances;

/// <summary>
/// 实体删除字段
/// </summary>
public interface IHasDeleted<T>
{
    T DeleterId { get; set; }

    DateTime? DeletionTime { get; set; }
}