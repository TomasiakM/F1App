using Domain.DDD;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;
internal abstract class GenericRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IAggregateRoot
    where TId : ValueObject
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _set;

    public GenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _set.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _set.AddRange(entities);
    }

    public void Update(TEntity entity)
    {
        _set.Update(entity);
    }

    public void UpdateRange(ICollection<TEntity> entities)
    {
        _set.UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _set.Remove(entity);
    }

    public void DeleteRange(ICollection<TEntity> entities)
    {
        _set.RemoveRange(entities);
    }

    public async Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _set.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _set.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _set.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _set.Where(predicate).ToListAsync(cancellationToken);
    }
}
