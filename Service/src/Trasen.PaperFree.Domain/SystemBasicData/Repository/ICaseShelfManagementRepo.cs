using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface ICaseShelfManagementRepo
    {
        Task<bool> AddAsync(CaseShelfManagement entity, CancellationToken cancellationToken);

        ValueTask<CaseShelfManagement?> FindById(string Id);

        // Task<bool> Update(CaseShelfManagement entity);
        bool Update(CaseShelfManagement entity);

        IQueryable<CaseShelfManagement> QueryAll();
    }
}