using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface ISysOperLogRepo
    {
        Task<bool> AddAsync(SysOperLog entity, CancellationToken cancellationToken);

        IQueryable<SysOperLog> QueryAll();

        ValueTask<SysOperLog?> FindById(string Id);
    }
}