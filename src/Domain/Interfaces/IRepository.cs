using Domain.DDD;
using System.Linq.Expressions;

namespace Domain.Interfaces;
public interface IRepository<TEntity, TId>
    where TEntity : class, IAggregateRoot
    where TId : ValueObject
{
    Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);
    void UpdateRange(ICollection<TEntity> entities);

    void Delete(TEntity entity);
    void DeleteRange(ICollection<TEntity> entities);
}
