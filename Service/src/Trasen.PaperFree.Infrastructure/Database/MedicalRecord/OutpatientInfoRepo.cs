using Trasen.PaperFree.Domain.MedicalRecord.Entity;
using Trasen.PaperFree.Domain.MedicalRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.MedicalRecord
{
    internal class OutpatientInfoRepo : IOutpatientInfoRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<OutpatientInfo> DbSet { get; }

        public OutpatientInfoRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<OutpatientInfo>();
        }
        public async Task<bool> AddAsync(OutpatientInfo entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public IQueryable<OutpatientInfo> QueryAll()
        {
            return DbSet.AsQueryable();
        }

        public async ValueTask<OutpatientInfo?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public bool Update(OutpatientInfo entity)
        {
            DbSet.Update(entity);
            return true;
        }

        public bool Update(List<OutpatientInfo> entity)
        {
            DbSet.UpdateRange(entity);
            return true;
        }
    }
}