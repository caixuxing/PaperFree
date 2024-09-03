using Trasen.PaperFree.Domain.BasicData;
using Trasen.PaperFree.Domain.BasicData.Repository;
using Trasen.PaperFree.Infrastructure.Database.DbContext;

namespace Trasen.PaperFree.Infrastructure.Database.BasicData
{
    internal class BasOrgMemberRepo : IBasOrgMemberRepo
    {
        private readonly BasicDataDbContext dbContext;

        /// <summary>
        /// 实体集合
        /// </summary>
        private DbSet<BaseOrgMember> DbSet { get; }

        public BasOrgMemberRepo(BasicDataDbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<BaseOrgMember>();
        }

        public IQueryable<BaseOrgMember> QueryAll()
        {
            return DbSet.AsQueryable();
        }
    }
}