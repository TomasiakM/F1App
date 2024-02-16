using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.Ratings;
using Domain.Aggregates.Ratings.Exceptions;
using Domain.Aggregates.UserDriverRatings;
using Domain.Aggregates.Users.ValueObjects;
using Domain.Interfaces;
using Moq;

namespace Test.Domain.Aggregates.Ratings;
public class RatingTests
{
    private readonly Mock<IDateProvider> _mockDateProvider;

    public RatingTests()
    {
        _mockDateProvider = new Mock<IDateProvider>();
    }

    [Fact]
    public void Rating_Create_ShouldCreateRating()
    {
        var raceWeekId = RaceWeekId.Create();
        var driverIds = new List<DriverId>
        {
            DriverId.Create(),
            DriverId.Create(),
            DriverId.Create(),
        };

        var date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan(1, 0, 0));

        _mockDateProvider
            .Setup(e => e.Now)
            .Returns(date);

        var rating = Rating.Create(raceWeekId, driverIds, _mockDateProvider.Object);

        Assert.Equal(raceWeekId, rating.RaceWeekId);
        Assert.Equal(3, rating.DriverIds.Count);
        Assert.Equal(3, rating.DriverRatings.Count);
        Assert.Equal(date.AddDays(3), rating.Finish);
        Assert.False(rating.IsSummarized);
    }

    [Fact]
    public void Rating_AddRatings_ShouldAddRatings()
    {
        var raceWeekId = RaceWeekId.Create();

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverIds = new List<DriverId>
        {
            driverId1,
            driverId2
        };

        var date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.Now)
            .Returns(date);

        var rating = Rating.Create(raceWeekId, driverIds, _mockDateProvider.Object);

        var userId1 = UserId.Create();
        var ratings1 = new List<UserDriverRating> {
            UserDriverRating.Create(rating.Id, driverId1, userId1, 10),
            UserDriverRating.Create(rating.Id, driverId2, userId1, 6)
        };

        var userId2 = UserId.Create();
        var ratings2 = new List<UserDriverRating> {
            UserDriverRating.Create(rating.Id, driverId1, userId2, 5),
            UserDriverRating.Create(rating.Id, driverId2, userId2, 2)
        };

        rating.AddRatings(ratings1);

        Assert.Equal(1, rating.DriverRatings.First(e => e.DriverId == driverId1).RatesCount);
        Assert.Equal(10, rating.DriverRatings.First(e => e.DriverId == driverId1).Rating);

        Assert.Equal(1, rating.DriverRatings.First(e => e.DriverId == driverId2).RatesCount);
        Assert.Equal(6, rating.DriverRatings.First(e => e.DriverId == driverId2).Rating);

        rating.AddRatings(ratings2);

        Assert.Equal(2, rating.DriverRatings.First(e => e.DriverId == driverId1).RatesCount);
        Assert.Equal(7.5, rating.DriverRatings.First(e => e.DriverId == driverId1).Rating);

        Assert.Equal(2, rating.DriverRatings.First(e => e.DriverId == driverId2).RatesCount);
        Assert.Equal(4, rating.DriverRatings.First(e => e.DriverId == driverId2).Rating);
    }

    [Fact]
    public void Rating_AddRatings_ShouldThrowInvalidDriverRatingsException()
    {
        var raceWeekId = RaceWeekId.Create();

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverIds = new List<DriverId>
        {
            driverId1,
            driverId2
        };

        var date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.Now)
            .Returns(date);

        var rating = Rating.Create(raceWeekId, driverIds, _mockDateProvider.Object);

        var userId = UserId.Create();
        var ratings = new List<UserDriverRating> {
            UserDriverRating.Create(rating.Id, driverId1, userId, 10),
        };

        Assert.Throws<InvalidDriverRatingsException>(() => rating.AddRatings(ratings));
    }

    [Fact]
    public void Rating_SummerizeDriversRatings_ShouldSummerizeRatings()
    {
        var raceWeekId = RaceWeekId.Create();

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverIds = new List<DriverId>
        {
            driverId1,
            driverId2
        };

        var date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.Now)
            .Returns(date);

        var rating = Rating.Create(raceWeekId, driverIds, _mockDateProvider.Object);

        var userId = UserId.Create();
        var ratings = new List<UserDriverRating> {
            UserDriverRating.Create(rating.Id, driverId1, userId, 10),
            UserDriverRating.Create(rating.Id, driverId2, userId, 6)
        };

        rating.SummerizeDriversRatings(ratings);

        Assert.Equal(1, rating.DriverRatings.First(e => e.DriverId == driverId1).RatesCount);
        Assert.Equal(10, rating.DriverRatings.First(e => e.DriverId == driverId1).Rating);

        Assert.Equal(1, rating.DriverRatings.First(e => e.DriverId == driverId2).RatesCount);
        Assert.Equal(6, rating.DriverRatings.First(e => e.DriverId == driverId2).Rating);
    }

    [Fact]
    public void Rating_SummerizeDriversRatings_ShouldThrowInvalidDriverRatingsException()
    {
        var raceWeekId = RaceWeekId.Create();

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverIds = new List<DriverId>
        {
            driverId1,
            driverId2
        };

        var date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.Now)
            .Returns(date);

        var rating = Rating.Create(raceWeekId, driverIds, _mockDateProvider.Object);

        var userId = UserId.Create();
        var ratings = new List<UserDriverRating> {
            UserDriverRating.Create(rating.Id, driverId1, userId, 10),
        };

        Assert.Throws<InvalidDriverRatingsException>(() => rating.SummerizeDriversRatings(ratings));
    }

    [Fact]
    public void Rating_IsReadyToSummerize_ShouldReturnFalse()
    {
        var raceWeekId = RaceWeekId.Create();

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverIds = new List<DriverId>
        {
            driverId1,
            driverId2
        };

        var date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.Now)
            .Returns(date);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(date.AddDays(2));

        var rating = Rating.Create(raceWeekId, driverIds, _mockDateProvider.Object);

        var drivers = new List<UserDriverRating>();


        Assert.False(rating.IsReadyToSummerize(_mockDateProvider.Object));
    }

    [Fact]
    public void Rating_IsReadyToSummerize_ShouldReturnFalse2()
    {
        var raceWeekId = RaceWeekId.Create();

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverIds = new List<DriverId>
        {
            driverId1,
            driverId2
        };

        var date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.Now)
            .Returns(date);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(date.AddHours(-4));

        var rating = Rating.Create(raceWeekId, driverIds, _mockDateProvider.Object);

        var drivers = new List<UserDriverRating>();


        Assert.False(rating.IsReadyToSummerize(_mockDateProvider.Object));
    }

    [Fact]
    public void Rating_IsReadyToSummerize_ShouldReturnTrue()
    {
        var raceWeekId = RaceWeekId.Create();

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverIds = new List<DriverId>
        {
            driverId1,
            driverId2
        };

        var date = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.Now)
            .Returns(date);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(date.AddDays(3));

        var rating = Rating.Create(raceWeekId, driverIds, _mockDateProvider.Object);

        var drivers = new List<UserDriverRating>();


        Assert.True(rating.IsReadyToSummerize(_mockDateProvider.Object));
    }
}
