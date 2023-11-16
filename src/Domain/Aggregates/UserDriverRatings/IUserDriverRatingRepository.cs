using Domain.Aggregates.UserDriverRatings.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.UserDriverRatings;
public interface IUserDriverRatingRepository : IRepository<UserDriverRating, UserDriverRatingId>
{
}
