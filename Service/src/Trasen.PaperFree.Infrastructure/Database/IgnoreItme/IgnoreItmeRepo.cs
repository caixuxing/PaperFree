using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.IgnoreItme.Entity;
using Trasen.PaperFree.Domain.IgnoreItme.Repository;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;
using Trasen.PaperFree.Infrastructure.Database.DbContext;
using Trasen.PaperFree.Infrastructure.EntityConfigs;

namespace Trasen.PaperFree.Infrastructure.Database.IgnoreItme
{
    internal class IgnoreItmeRepo: IIgnoreItmeRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<IgnoreItmeTable> DbSet { get; }

        public IgnoreItmeRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<IgnoreItmeTable>();
        }

        public async Task<bool> AddAsync(IgnoreItmeTable entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public IQueryable<IgnoreItmeTable> QueryAll()
        {
            return DbSet;
        }

        public bool Update(IgnoreItmeTable entity)
        {
            DbSet.Update(entity);
            return true;
        }

        public async ValueTask<IgnoreItmeTable?> FindById(string id)
        {
            return await DbSet.FindAsync(id);
        }

    }
}
