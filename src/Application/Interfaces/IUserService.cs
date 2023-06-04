using Domain.Aggregates.Users.ValueObjects;

namespace Application.Interfaces;
public interface IUserService
{
    UserId GetUserId();
    Task Register(string username, string password, string email, CancellationToken cancellationToken = default);
    Task UpdatePassword(string password, string newPassword, CancellationToken cancellationToken);
}
