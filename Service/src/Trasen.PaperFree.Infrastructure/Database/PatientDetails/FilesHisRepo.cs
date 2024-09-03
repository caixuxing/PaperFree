using Trasen.PaperFree.Domain.FileTable.Entity;
using Trasen.PaperFree.Domain.PatientDetails.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.PatientDetails
{
    internal class FilesHisRepo : IFilesHisRepo
    {
        private readonly AppDbContext dbContext;
        private DbSet<FilesHis> DbSet { get; }

        public FilesHisRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<FilesHis>();
        }

        public async Task<bool> AddAsync(FilesHis entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public IQueryable<FilesHis> QueryAll()
        {
            return DbSet;
        }
        public async ValueTask<FilesHis?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }
        public async ValueTask<FilesHis?> FindByFileId(string FileId)
        {
            return await DbSet.FindAsync(FileId);
        }
        public bool Update(FilesHis entity)
        {
            DbSet.Update(entity);
            return true;
        }
    }
}