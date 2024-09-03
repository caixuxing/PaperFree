using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface IBaseBorrowModeRepo
    {
        Task<bool> AddAsync(BaseBorrowMode entity, CancellationToken cancellationToken);

        ValueTask<BaseBorrowMode?> FindById(string Id);

        //  Task<bool> Update(BaseBorrowMode entity);
        bool Update(BaseBorrowMode entity);

        IQueryable<BaseBorrowMode> QueryAll();
    }
}