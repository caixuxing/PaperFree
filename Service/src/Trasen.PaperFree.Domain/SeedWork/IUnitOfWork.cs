using Microsoft.EntityFrameworkCore;

namespace Trasen.PaperFree.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        DbContext dbContext { get; }
    }
}