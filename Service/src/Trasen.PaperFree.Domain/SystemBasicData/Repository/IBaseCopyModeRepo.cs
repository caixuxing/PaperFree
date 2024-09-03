using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface IBaseCopyModeRepo
    {
        Task<bool> AddAsync(BaseCopyMode entity, CancellationToken cancellationToken);

        ValueTask<BaseCopyMode?> FindById(string Id);

        Task<bool> Update(BaseCopyMode entity);
    }
}