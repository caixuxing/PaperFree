using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.ProcessRecord
{
    public class ProcessNodeRepo : IProcessNodeRepo
    {
        private readonly AppDbContext dbContext;
        private DbSet<ProcessNode> DbSet { get; }
        private DbSet<NodeApprover> NodesDbSet { get; }

        public ProcessNodeRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<ProcessNode>();
            NodesDbSet = dbContext.Set<NodeApprover>();
        }

        public async Task<bool> AddAsync(ProcessNode entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            if (entity.NodeApprovers is not null)
                await NodesDbSet.AddRangeAsync(entity.NodeApprovers);
            return true;
        }

        public async ValueTask<ProcessNode?> FindById(string Id)
        {
            var entity = await DbSet.FindAsync(Id);
            if (entity is not null)
                entity.NodeApprovers = await NodesDbSet.Where(_ => _.ProcessNodeId == entity.Id).ToListAsync();
            return entity;
        }

        public async Task<bool> Update(ProcessNode entity)
        {
            DbSet.Update(entity);
            if (entity is not null && entity.NodeApprovers.Any())
            {
                var old = await NodesDbSet.Where(_ => _.ProcessNodeId == entity.Id).ToListAsync();

                foreach (var item in entity.NodeApprovers)
                {
                    if (!old.Any(_ => _.ApproverId == item.ApproverId && _.ProcessNodeId == item.ProcessNodeId))
                        await NodesDbSet.AddAsync(item);
                }

                foreach (var item in old)
                {
                    if (entity.NodeApprovers.Any(_ => _.ApproverId == item.ApproverId && _.ApproverId == item.ApproverId))
                        item.IsDeleted = true;
                    NodesDbSet.Update(item);
                }
            }
            return true;
        }

        public IQueryable<ProcessNode> QueryAll()
        {
            return DbSet;
        }

        public async Task<bool> AddAsync(List<ProcessNode> entity, CancellationToken cancellationToken)
        {
            await DbSet.AddRangeAsync(entity, cancellationToken);
            var NodeApprovers = entity.SelectMany(x => x.NodeApprovers).ToList();
            if (NodeApprovers is not null)
                await NodesDbSet.AddRangeAsync(NodeApprovers);
            return true;
        }

        public bool SoftDelete(List<ProcessNode> entity)
        {
            DbSet.UpdateRange(entity);
            entity.ForEach(item =>
            {
                item.ChangeDelete();
                DbSet.Update(item);
                item.NodeApprovers.ToList().ForEach(_ =>
                {
                    _.ChangeDelete();
                    NodesDbSet.Update(_);
                });
            });
            return true;
        }

    }
}