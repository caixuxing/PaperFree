using Trasen.PaperFree.Domain.ArchiveRecord.Entity;

namespace Trasen.PaperFree.Domain.ArchiveRecord.Repository
{
    public interface IArchiveApplyRepo
    {
        Task<bool> AddAsync(ArchiveApply entity, CancellationToken cancellationToken);

        Task<bool> AddAsyncList(List<ArchiveApply> entity, CancellationToken cancellationToken);

        IQueryable<ArchiveApply> QueryAll();

        ValueTask<ArchiveApply?> FindById(string Id);

        bool Update(ArchiveApply entity);
    }
}