using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.ArchiveRecord
{
    internal class ArchiveApplyRepo : IArchiveApplyRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<ArchiveApply> DbSet { get; }

        public ArchiveApplyRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<ArchiveApply>();
        }

        public async Task<bool> AddAsync(ArchiveApply entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public IQueryable<ArchiveApply> QueryAll()
        {
            return DbSet;
        }

        public async ValueTask<ArchiveApply?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public bool Update(ArchiveApply entity)
        {
            DbSet.Update(entity);
            return true;
        }

        public Task<bool> AddAsyncList(List<ArchiveApply> entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}