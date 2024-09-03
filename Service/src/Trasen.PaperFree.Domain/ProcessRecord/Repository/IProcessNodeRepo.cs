using Trasen.PaperFree.Domain.ProcessRecord.Entity;

namespace Trasen.PaperFree.Domain.ProcessRecord.Repository
{
    public interface IProcessNodeRepo
    {
        Task<bool> AddAsync(ProcessNode entity, CancellationToken cancellationToken);

        Task<bool> AddAsync(List<ProcessNode> entity, CancellationToken cancellationToken);

        ValueTask<ProcessNode?> FindById(string Id);

        Task<bool> Update(ProcessNode entity);


       bool SoftDelete(List<ProcessNode> entity);

        IQueryable<ProcessNode> QueryAll();
    }
}