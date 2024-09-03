using Trasen.PaperFree.Domain.FileTable.Entity;

namespace Trasen.PaperFree.Domain.PatientDetails.Repository
{
    public interface IFilesOtherRepo
    {
        Task<bool> AddAsync(FilesOther entity, CancellationToken cancellationToken);

        IQueryable<FilesOther> QueryAll();

        ValueTask<FilesOther?> FindById(string id);

        bool Update(FilesOther entity);
    }
}