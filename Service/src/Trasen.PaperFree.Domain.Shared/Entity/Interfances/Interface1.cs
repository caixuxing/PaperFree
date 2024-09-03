namespace Trasen.PaperFree.Domain.Shared.Entity.Interfances
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}