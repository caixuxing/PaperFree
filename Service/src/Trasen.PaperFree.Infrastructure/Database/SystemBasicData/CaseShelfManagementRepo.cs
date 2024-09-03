using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.SystemBasicData
{
    internal class CaseShelfManagementRepo : ICaseShelfManagementRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<CaseShelfManagement> DbSet { get; }

        public CaseShelfManagementRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<CaseShelfManagement>();
        }

        public async Task<bool> AddAsync(CaseShelfManagement entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public async ValueTask<CaseShelfManagement?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<CaseShelfManagement> QueryAll()
        {
            return DbSet;
        }

        public bool Update(CaseShelfManagement entity)
        {
            DbSet.Update(entity);
            return true;
        }

        //Task<bool> ICaseShelfManagementRepo.Update(CaseShelfManagement entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}