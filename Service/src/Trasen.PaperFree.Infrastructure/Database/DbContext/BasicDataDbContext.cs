using Microsoft.Extensions.Logging;
using Trasen.PaperFree.Domain.BasicData;

namespace Trasen.PaperFree.Infrastructure.Database.DbContext
{
    /// <summary>
    /// 基础数据Db上下文
    /// </summary>
    public class BasicDataDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        private readonly DbConnectionOption _dbConnection;

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        public BasicDataDbContext(IOptions<DbConnectionOption> options)
        {
            _dbConnection = options.Value;
        }

        private DbSet<BaseOrgMember> basOrgMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle(_dbConnection.BasicDataConnection, p => p.UseOracleSQLCompatibility("11"))
                     .EnableSensitiveDataLogging(false)
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Error);
            }
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}