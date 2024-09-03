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
    internal class InPatientInfoRepo: IInpatientInfoRepo
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<InpatientInfo> DbSet { get; }

        public InPatientInfoRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<InpatientInfo>();
        }

        public IQueryable<InpatientInfo> QueryAll()
        {
            return DbSet.AsQueryable();
        }

        public async ValueTask<InpatientInfo?> FindById(string Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public bool Update(InpatientInfo entity)
        {
            DbSet.Update(entity);
            return true;
        }

        public bool Update(List<InpatientInfo> entity)
        {
            DbSet.UpdateRange(entity);
            return true;
        }
    }
}