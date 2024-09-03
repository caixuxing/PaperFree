using Trasen.PaperFree.Domain.RecallRecord.Entity;

namespace Trasen.PaperFree.Domain.RecallRecord.Repository
{
    public interface IRecallApplyRepo
    {
        Task<bool> AddAsync(RecallApply entity, CancellationToken cancellationToken);


        Task<bool> AddAsync(RecallApprover entity, CancellationToken cancellationToken);

        IQueryable<RecallApply> QueryAll();

        ValueTask<RecallApply?> FindById(string Id);

        bool Update(RecallApply entity);
    }
}