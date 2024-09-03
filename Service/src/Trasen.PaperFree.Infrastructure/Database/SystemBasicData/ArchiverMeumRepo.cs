using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.SystemBasicData
{
    internal class ArchiverMeumRepo : IArchiverMeumRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<ArchiverMeum> DbSet { get; }

        public ArchiverMeumRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<ArchiverMeum>();
        }

        public async Task<bool> AddAsync(ArchiverMeum entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public async Task<bool> AddAsyncList(List<ArchiverMeum> entity, CancellationToken cancellationToken)
        {
            await DbSet.AddRangeAsync(entity, cancellationToken);
            return true;
        }

        public async ValueTask<ArchiverMeum?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<ArchiverMeum> QueryAll()
        {
            return DbSet;
        }

        public bool Update(ArchiverMeum entity)
        {
            DbSet.Update(entity);
            return true;
        }

        public bool UpdateList(List<ArchiverMeum> entity)
        {
            DbSet.UpdateRange(entity);
            return true;
        }
    }
}