using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.SystemBasicData
{
    internal class BaseBorrowModeRepo : IBaseBorrowModeRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<BaseBorrowMode> DbSet { get; }

        public BaseBorrowModeRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<BaseBorrowMode>();
        }

        public async Task<bool> AddAsync(BaseBorrowMode entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public async ValueTask<BaseBorrowMode?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<BaseBorrowMode> QueryAll()
        {
            return DbSet;
        }

        public bool Update(BaseBorrowMode entity)
        {
            DbSet.Update(entity);
            return true;
        }
    }
}