using Trasen.PaperFree.Domain.ArchiveRecord.Entity;

namespace Trasen.PaperFree.Domain.ArchiveRecord.Repository
{
    public interface IArchiverMeumRepo
    {
        Task<bool> AddAsync(ArchiverMeum entity, CancellationToken cancellationToken);

        Task<bool> AddAsyncList(List<ArchiverMeum> entity, CancellationToken cancellationToken);

        bool UpdateList(List<ArchiverMeum> entity);

        IQueryable<ArchiverMeum> QueryAll();

        ValueTask<ArchiverMeum?> FindById(string Id);

        bool Update(ArchiverMeum entity);
    }
}