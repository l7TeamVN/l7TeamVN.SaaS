using l7TeamVN.SaaS.Domain.Entities;
using l7TeamVN.SaaS.Domain.Repositories;
using l7TeamVN.SaaS.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace l7TeamVN.SaaS.Persistence.Repositories;


public abstract class GenericRepository<TDbContext, TEntity, TKey>(TDbContext dbContext) : IGenericRepository<TEntity, TKey> where TDbContext : DbContextUnitOfWork<TDbContext> where TEntity : AggregateRoot<TKey>
{
    private readonly TDbContext _dbContext = dbContext;

    protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task<TKey?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await UnitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<TKey?> CreateOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (EqualityComparer<TKey>.Default.Equals(entity.Id, default))
        {
            return await CreateAsync(entity, cancellationToken);
        }
        else
        {
            return await UpdateAsync(entity, cancellationToken);
        }
    }

    public async Task<TKey?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await UnitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        return await UnitOfWork.SaveChangesAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        if (EqualityComparer<TKey>.Default.Equals(id, default))
        {
            return null;
        }
        return await DbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }
}
