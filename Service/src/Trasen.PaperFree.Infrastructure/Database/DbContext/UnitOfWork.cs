namespace Trasen.PaperFree.Infrastructure.Database.DbContext;

/// <summary>
///
/// </summary>
/// <typeparam name="TdbContext"></typeparam>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    Microsoft.EntityFrameworkCore.DbContext IUnitOfWork.dbContext => dbContext;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync();
    }
}