using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.SystemBasicData
{
    internal class SysOperLogRepo : ISysOperLogRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<SysOperLog> DbSet { get; }

        public SysOperLogRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<SysOperLog>();
        }

        public async Task<bool> AddAsync(SysOperLog entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public IQueryable<SysOperLog> QueryAll()
        {
            return DbSet.AsQueryable();
        }

        public async ValueTask<SysOperLog?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }
    }
}