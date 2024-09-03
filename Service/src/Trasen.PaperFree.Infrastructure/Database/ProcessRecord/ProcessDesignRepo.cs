using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.ProcessRecord
{
    public class ProcessDesignRepo : IProcessDesignRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<ProcessDesign> DbSet { get; }

        private DbSet<ProcessNode> ProcessNodeDbSet { get; }
        private DbSet<NodeApprover> NodeApproverDbSet { get; }

        public ProcessDesignRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<ProcessDesign>();
            ProcessNodeDbSet = dbContext.Set<ProcessNode>();
            NodeApproverDbSet = dbContext.Set<NodeApprover>();
        }
        public async Task<bool> AddAsync(ProcessDesign entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            if (entity.ProcessNodes is not null)
            {
                await ProcessNodeDbSet.AddRangeAsync(entity.ProcessNodes);
                entity.ProcessNodes.ToList().ForEach(async item =>
                {
                    await NodeApproverDbSet.AddRangeAsync(item.NodeApprovers);
                });
            }
            return true;
        }

        public async ValueTask<ProcessDesign?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public IQueryable<ProcessDesign> QueryAll()
        {
            return DbSet;
        }

        public bool Update(ProcessDesign entity)
        {
            DbSet.Update(entity);
       /*     if (entity.ProcessNodes is not null)
            {
                 ProcessNodeDbSet.UpdateRange(entity.ProcessNodes);
                entity.ProcessNodes.ToList().ForEach( item =>
                {
                     NodeApproverDbSet.UpdateRange(item.NodeApprovers);
                });
            }*/
            return true;
        }
    }
}