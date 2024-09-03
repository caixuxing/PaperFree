using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Reflection;
using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.Appsettings;
using Trasen.PaperFree.Domain.Shared.Config;
using Trasen.PaperFree.Domain.Shared.Entity.Interfances;

namespace Trasen.PaperFree.Infrastructure.Database.DbContext;

/// <summary>
/// 无纸化DbContext
/// </summary>
public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    /*控制台打印SQL 引入下列包
       1.Microsoft.Extensions.Logging
       2.Microsoft.Extensions.Logging.Console
     */

    public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
    {
        Console.ForegroundColor = ConsoleColor.Red;
        builder.AddConsole();
    });

    private readonly DbConnectionOption _dbConnection;
    private readonly SlaveRoundRobin _slaveRoundRobin;
    private readonly IOptions<DbConnectionOption> _options;
    private readonly IMediator _mediator;
    private readonly ICurrentUser currentUser;

    private readonly IGuidGenerator _guidGenerator;

    /// <summary>
    ///
    /// </summary>
    /// <param name="options"></param>
    /// <param name="slaveRoundRobin"></param>
    /// <param name="mediator"></param>
    /// <param name="currentUser"></param>
    /// <param name="guidGenerator"></param>
    public AppDbContext(IOptions<DbConnectionOption> options, SlaveRoundRobin slaveRoundRobin,
        IMediator mediator, ICurrentUser currentUser, IGuidGenerator guidGenerator)
    {
        _dbConnection = options.Value;
        _slaveRoundRobin = slaveRoundRobin;
        _options = options;
        _mediator = mediator;
        this.currentUser = currentUser;
        _guidGenerator = guidGenerator;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseOracle(_dbConnection.MasterConnection, p => p.UseOracleSQLCompatibility("11"))
                 .EnableSensitiveDataLogging(false)
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Error);
        }
        var SystemConfig = Appsetting.Instance.GetSection("SystemConfig").Get<SystemConfig>();
        if (SystemConfig.IsSqlLogConsole)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // 将当前正在运行的程序集中所有继承自IEntityTypeConfiguration的类加载到EF的配置中
        var assembly = this.GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }

    /// <summary>
    /// 主库
    /// </summary>
    public Microsoft.EntityFrameworkCore.DbContext ToMaster()
    {
        //把链接字符串设为读写（主库）
        this.Database.GetDbConnection().ConnectionString = this._dbConnection.MasterConnection;
        return this;
    }

    /// <summary>
    /// 从库
    /// </summary>
    public Microsoft.EntityFrameworkCore.DbContext ToSlave()
    {
        var connection = Database.GetDbConnection();
        if (connection.State.HasFlag(ConnectionState.Open))
        {
            //连接未关闭的时候的切换方式
            connection.ChangeDatabase(_slaveRoundRobin.GetNext());
        }
        else
        {
            connection.ConnectionString = _slaveRoundRobin.GetNext();
        }
        return this;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetSystemField();
        var domainEntities = this.ChangeTracker.Entries<IDomainEvents>().Where(x => x.Entity.GetDomainEvents().Any());
        var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents()).ToList();
        domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetSystemField()
    {
        foreach (var item in ChangeTracker.Entries())
        {
            if (item.Entity is FullRoot)
            {
                var entity = (FullRoot)item.Entity;
                if (item.State == EntityState.Added)
                {
                    if (entity.Id == Guid.Empty.ToString())
                    {
                        entity.Id = _guidGenerator.Create().ToString();
                    }
                    entity.CreationTime = DateTime.Now;
                    entity.CreatorId = currentUser.Id;
                    entity.IsDeleted = false;
                }
                else if (item.State == EntityState.Modified)
                {
                    if (entity.IsDeleted)
                    {
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyId = currentUser.Id;
                        entity.IsDeleted = true;
                    }
                    else
                    {
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyId = currentUser.Id;
                    }
                }
            }
        }
    }
}

public static class SoftDeleteQueryExtension
{
    public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
    {
        MethodInfo methodToCall = typeof(SoftDeleteQueryExtension).GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { });
        entityData.SetQueryFilter((LambdaExpression)filter);
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDelete
    {
        Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
        return filter;
    }
}