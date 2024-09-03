using Trasen.PaperFree.Domain.ArchiveRecord.Entity;

namespace Trasen.PaperFree.Domain.ArchiveRecord.Repository
{
    public interface IArchiveApproverRepo
    {
        Task<bool> AddAsync(ArchiveApprover entity, CancellationToken cancellationToken);

        IQueryable<ArchiveApprover> QueryAll();
    }
}