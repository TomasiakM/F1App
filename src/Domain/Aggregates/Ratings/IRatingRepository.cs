using Domain.Aggregates.Ratings.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Ratings;
public interface IRatingRepository : IRepository<Rating, RatingId>
{
}
