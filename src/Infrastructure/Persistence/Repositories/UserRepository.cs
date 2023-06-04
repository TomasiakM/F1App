using Domain.Aggregates.Users;
using Domain.Aggregates.Users.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class UserRepository : GenericRepository<User, UserId>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
