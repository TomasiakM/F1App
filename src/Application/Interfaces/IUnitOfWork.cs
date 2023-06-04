using Domain.Aggregates.Users;

namespace Application.Interfaces;
public interface IUnitOfWork
{
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
