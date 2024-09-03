namespace Trasen.PaperFree.Domain.Shared.Entity.Interfances;

/// <summary>
/// 版本号实体接口
/// </summary>
public interface IHasVersion
{
    /// <summary>
    /// 版本号
    /// </summary>
    byte[] RowVersion { get; set; }
}