using Domain.Aggregates.UserDriverRatings;
using Domain.Aggregates.UserDriverRatings.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class UserDriverRatingRepository : GenericRepository<UserDriverRating, UserDriverRatingId>, IUserDriverRatingRepository
{
    public UserDriverRatingRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
