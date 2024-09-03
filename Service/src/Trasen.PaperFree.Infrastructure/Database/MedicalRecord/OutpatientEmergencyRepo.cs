using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;
using Trasen.PaperFree.Domain.MedicalRecord.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.MedicalRecord
{
    internal class OutpatientEmergencyRepo : IOutpatientEmergencyRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 门急诊实体集合
        /// </summary>
        private DbSet<OutpatientEmergency> DbSet { get; }

        public OutpatientEmergencyRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<OutpatientEmergency>();
        }
        public async Task<bool> AddAsync(OutpatientEmergency entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public IQueryable<OutpatientEmergency> QueryAll()
        {
            return DbSet.AsQueryable();
        }

        public async ValueTask<OutpatientEmergency?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public bool Update(OutpatientEmergency entity)
        {
            DbSet.Update(entity);
            return true;
        }
    }
}