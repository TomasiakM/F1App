using Application.Interfaces;
using Domain.Aggregates.Users;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    public IUserRepository Users { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        Users = new UserRepository(_dbContext);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
