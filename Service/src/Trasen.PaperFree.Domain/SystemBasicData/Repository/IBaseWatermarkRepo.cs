using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface IBaseWatermarkRepo
    {
        Task<bool> AddAsync(BaseWatermark entity, CancellationToken cancellationToken);

        ValueTask<BaseWatermark?> FindById(string Id);

        // Task<bool> Update(BaseWatermark entity);
        bool Update(BaseWatermark entity);

        IQueryable<BaseWatermark> QueryAll();
    }
}