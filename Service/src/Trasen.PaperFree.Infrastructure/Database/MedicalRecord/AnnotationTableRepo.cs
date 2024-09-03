using Trasen.PaperFree.Domain.MedicalRecord.Entity;
using Trasen.PaperFree.Domain.MedicalRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.MedicalRecord
{
    internal class AnnotationTableRepo : IAnnotationTableRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<AnNotAtionTable> DbSet { get; }

        public AnnotationTableRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<AnNotAtionTable>();
        }

        public async Task<bool> AddAsync(AnNotAtionTable entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        //public async ValueTask<AnNotAtionTable?> FindById(string Id)
        //{
        //    return await DbSet.FindAsync(Id);
        //}

        public IQueryable<AnNotAtionTable> QueryAll()
        {
            return DbSet;
        }

        public bool Update(AnNotAtionTable entity)
        {
            DbSet.Update(entity);
            return true;
        }

        public async ValueTask<AnNotAtionTable?> FindId(string id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}