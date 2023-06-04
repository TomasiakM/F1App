using Domain.Aggregates.Users.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Users;
public interface IUserRepository : IRepository<User, UserId>
{
}
