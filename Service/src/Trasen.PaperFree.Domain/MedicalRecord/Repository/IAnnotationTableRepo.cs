using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Domain.MedicalRecord.Repository
{
    public interface IAnnotationTableRepo
    {
        Task<bool> AddAsync(AnNotAtionTable entity, CancellationToken cancellationToken);

        //Task<AnNotAtionTable?> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

        IQueryable<AnNotAtionTable> QueryAll();

        //ValueTask<AnNotAtionTable?> FindAsync(Guid id, CancellationToken cancellationToken);

        ValueTask<AnNotAtionTable?> FindId(string id);

        bool Update(AnNotAtionTable entity);
    }
}