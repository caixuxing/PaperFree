using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.SystemBasicData
{
    internal class PayConfigRepo : IPayConfigRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<PayConfig> DbSet { get; }

        public PayConfigRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<PayConfig>();
        }

        public async Task<bool> AddAsync(PayConfig entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public async ValueTask<PayConfig?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<PayConfig> QueryAll()
        {
            return DbSet;
        }

        public bool Update(PayConfig entity)
        {
            DbSet.Update(entity);
            return true;
        }
    }
}