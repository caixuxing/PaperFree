using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.SystemBasicData
{
    internal class EssentialDocumentRepo : IEssentialDocumentsRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<EssentialDocuments> DbSet { get; }

        public EssentialDocumentRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<EssentialDocuments>();
        }

        public async Task<bool> AddAsync(EssentialDocuments entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public async ValueTask<EssentialDocuments?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<EssentialDocuments> QueryAll()
        {
            return DbSet;
        }

        public async Task<bool> AddAsync(List<EssentialDocuments> entity, CancellationToken cancellationToken)
        {
            await DbSet.AddRangeAsync(entity, cancellationToken);
            return true;
        }

        public bool Update(EssentialDocuments entity)
        {
            DbSet.Update(entity);
            return true;
        }

        public async Task<bool> AddAsyncList(List<EssentialDocuments> entity, CancellationToken cancellationToken)
        {
            await DbSet.AddRangeAsync(entity, cancellationToken);
            return true;
        }

        public bool Update(List<EssentialDocuments> entity)
        {
            DbSet.UpdateRange(entity);
            return true;
        }

        //public async Task<bool> UpdateAsync(List<EssentialDocuments> entity)
        //{
        //    DbSet.UpdateRange(entity);
        //    return true;
        //}

        //Task<bool> IEssentialDocumentsRepo.Update(EssentialDocuments entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}