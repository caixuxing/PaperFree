using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface IPayConfigRepo
    {
        Task<bool> AddAsync(PayConfig entity, CancellationToken cancellationToken);

        ValueTask<PayConfig?> FindById(string Id);

        bool Update(PayConfig entity);

        IQueryable<PayConfig> QueryAll();
    }
}