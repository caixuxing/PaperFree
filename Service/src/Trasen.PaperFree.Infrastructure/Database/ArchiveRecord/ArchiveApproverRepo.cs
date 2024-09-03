using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.ArchiveRecord
{
    internal class ArchiveApproverRepo : IArchiveApproverRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<ArchiveApprover> DbSet { get; }

        public ArchiveApproverRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<ArchiveApprover>();
        }

        public async Task<bool> AddAsync(ArchiveApprover entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public IQueryable<ArchiveApprover> QueryAll()
        {
            return DbSet;
        }
    }
}