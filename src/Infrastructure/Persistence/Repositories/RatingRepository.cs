using Domain.Aggregates.Ratings;
using Domain.Aggregates.Ratings.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class RatingRepository : GenericRepository<Rating, RatingId>, IRatingRepository
{
    public RatingRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
