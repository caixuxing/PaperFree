using Trasen.PaperFree.Domain.FileTable.Entity;

namespace Trasen.PaperFree.Domain.PatientDetails.Repository
{
    public interface IFilesHisRepo
    {
        Task<bool> AddAsync(FilesHis entity, CancellationToken cancellationToken);

        IQueryable<FilesHis> QueryAll();

        ValueTask<FilesHis?> FindById(string Id);
        bool Update(FilesHis entity);
    }
}