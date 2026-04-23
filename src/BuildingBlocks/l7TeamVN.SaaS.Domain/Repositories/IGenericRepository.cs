using l7TeamVN.SaaS.Domain.Entities;

namespace l7TeamVN.SaaS.Domain.Repositories;

public interface IGenericRepository<TEntity, TKey> where TEntity : AggregateRoot<TKey>
{
    IUnitOfWork UnitOfWork { get; }

    Task<TKey?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TKey?> CreateOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TKey?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}

