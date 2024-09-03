using Trasen.PaperFree.Domain.ProcessRecord.Entity;

namespace Trasen.PaperFree.Domain.ProcessRecord.Repository
{
    public interface IProcessDesignRepo
    {
        ValueTask<ProcessDesign?> FindById(string Id);

        Task<bool> AddAsync(ProcessDesign entity, CancellationToken cancellationToken);

        bool Update(ProcessDesign entity);

        IQueryable<ProcessDesign> QueryAll();
    }
}