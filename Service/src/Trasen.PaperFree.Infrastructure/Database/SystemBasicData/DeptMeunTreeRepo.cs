using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.SystemBasicData
{
    internal class DeptMeunTreeRepo : IDeptMenuTreeRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<DeptMeMenuTreeEntity> DbSet { get; }

        public DeptMeunTreeRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<DeptMeMenuTreeEntity>();
        }

        public async Task<bool> AddAsync(DeptMeMenuTreeEntity entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public async ValueTask<DeptMeMenuTreeEntity?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<DeptMeMenuTreeEntity> QueryAll()
        {
            return DbSet;
        }

        public async Task<bool> AddAsync(List<DeptMeMenuTreeEntity> entity, CancellationToken cancellationToken)
        {
            await DbSet.AddRangeAsync(entity, cancellationToken);
            return true;
        }

        public bool Update(DeptMeMenuTreeEntity entity)
        {
            DbSet.Update(entity);
            return true;
        }

        public async Task<bool> AddAsyncList(List<DeptMeMenuTreeEntity> entity, CancellationToken cancellationToken)
        {
            await DbSet.AddRangeAsync(entity, cancellationToken);
            return true;
        }

        public bool Update(List<DeptMeMenuTreeEntity> entity)
        {
            DbSet.UpdateRange(entity);
            return true;
        }
    }
}