using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.Ratings.ValueObjects;
using Domain.Aggregates.UserDriverRatings.ValueObjects;
using Domain.Aggregates.Users.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.UserDriverRatings;
public sealed class UserDriverRating : AggregateRoot<UserDriverRatingId>
{
    public RatingId RatingId { get; private set; }
    public DriverId DriverId { get; private set; }
    public UserId UserId { get; private set; }
    public int Rating { get; private set; }


    private UserDriverRating(RatingId ratingId, DriverId driverId, UserId userId, int rating) 
        : base(UserDriverRatingId.Create()) 
    {
        RatingId = ratingId;
        DriverId = driverId;
        UserId = userId;
        Rating = rating;
    }

    public static UserDriverRating Create(RatingId ratingId, DriverId driverId, UserId userId, int rating)
        => new(ratingId, driverId, userId, rating);

    #pragma warning disable CS8618
    private UserDriverRating() : base(UserDriverRatingId.Create()) { }
    #pragma warning restore CS8618
}
