using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.Ratings.ValueObjects;
using Domain.Aggregates.UserDriverRatings;
using Domain.Aggregates.Users.ValueObjects;

namespace Test.Domain.Aggregates.UserDriverRatings;
public class UserDriverRatingTests
{
    [Fact]
    public void UserDriverRating_Create_ShouldCreateUserDriverRating()
    {
        var ratingId = RatingId.Create();
        var driverId = DriverId.Create();
        var userId = UserId.Create();
        var rating = 7;

        var userDriverRating = UserDriverRating.Create(ratingId, driverId, userId, rating);

        Assert.Equal(ratingId, userDriverRating.RatingId);
        Assert.Equal(driverId, userDriverRating.DriverId);
        Assert.Equal(userId, userDriverRating.UserId);
        Assert.Equal(rating, userDriverRating.Rating);
    }
}
