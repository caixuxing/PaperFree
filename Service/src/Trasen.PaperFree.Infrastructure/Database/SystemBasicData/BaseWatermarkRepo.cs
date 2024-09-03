using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.SystemBasicData
{
    internal class BaseWatermarkRepo : IBaseWatermarkRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<BaseWatermark> DbSet { get; }

        public BaseWatermarkRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<BaseWatermark>();
        }

        public async Task<bool> AddAsync(BaseWatermark entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public async ValueTask<BaseWatermark?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<BaseWatermark> QueryAll()
        {
            return DbSet;
        }

        public bool Update(BaseWatermark entity)
        {
            DbSet.Update(entity);
            return true;
        }
    }
}