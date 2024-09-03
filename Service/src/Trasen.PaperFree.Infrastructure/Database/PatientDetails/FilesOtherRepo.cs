using Trasen.PaperFree.Domain.FileTable.Entity;
using Trasen.PaperFree.Domain.PatientDetails.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.PatientDetails
{
    internal class FilesOtherRepo : IFilesOtherRepo
    {
        private readonly AppDbContext dbContext;
        private DbSet<FilesOther> DbSet { get; }

        public FilesOtherRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<FilesOther>();
        }

        public async Task<bool> AddAsync(FilesOther entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public async ValueTask<FilesOther?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }
        public IQueryable<FilesOther> QueryAll()
        {
            return DbSet;
        }

        public bool Update(FilesOther entity)
        {
            DbSet.Update(entity);
            return true;
        }
    }
}