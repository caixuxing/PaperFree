using Trasen.PaperFree.Domain.RecallRecord.Entity;
using Trasen.PaperFree.Domain.RecallRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.RecallRecord
{
    internal class RecallApplyRepo : IRecallApplyRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<RecallApply> DbSet { get; }

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<RecallApplyAndArchival> DbRecallApplyAndArchivalsSet { get; }


        private DbSet<RecallApprover> DbRecallApproverSet { get; }

        public RecallApplyRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<RecallApply>();

            DbRecallApplyAndArchivalsSet = dbContext.Set<RecallApplyAndArchival>();
            DbRecallApproverSet=dbContext.Set<RecallApprover>();
        }

        public async Task<bool> AddAsync(RecallApply entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity);
            if (entity.RecallApplyAndArchivals.Any())
                await DbRecallApplyAndArchivalsSet.AddRangeAsync(entity.RecallApplyAndArchivals);
            return true;
        }

        public async ValueTask<RecallApply?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<RecallApply> QueryAll()
        {
            return DbSet;
        }

        public bool Update(RecallApply entity)
        {
            DbSet.Update(entity);
            if (entity.RecallApplyAndArchivals.Any())
                DbRecallApplyAndArchivalsSet.UpdateRange(entity.RecallApplyAndArchivals);
            return true;
        }

        public async Task<bool> AddAsync(RecallApprover entity, CancellationToken cancellationToken)
        {
            await DbRecallApproverSet.AddAsync(entity);
            return true;
        }
    }
}