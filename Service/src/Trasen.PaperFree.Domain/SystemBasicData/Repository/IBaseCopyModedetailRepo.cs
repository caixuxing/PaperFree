using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface IBaseCopyModedetailRepo
    {
        Task<bool> AddAsync(BaseCopyModedetail entity, CancellationToken cancellationToken);

        ValueTask<BaseCopyModedetail?> FindById(string Id);

        Task<bool> Update(BaseCopyModedetail entity);
    }
}