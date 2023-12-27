using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.Ratings.Entities;
using Domain.Aggregates.Ratings.Exceptions;
using Domain.Aggregates.Ratings.ValueObjects;
using Domain.Aggregates.UserDriverRatings;
using Domain.DDD;
using Domain.Interfaces;

namespace Domain.Aggregates.Ratings;
public sealed class Rating : AggregateRoot<RatingId>
{
    private readonly int _ratingExpiryDays = 3;

    private List<DriverId> _driverIds = new();
    private List<DriverRating> _driverRatings = new();

    public RaceWeekId RaceWeekId { get; private set; }
    public DateTimeOffset Finish { get; private set; }
    public bool IsSummarized { get; private set; }

    public IReadOnlyList<DriverId> DriverIds => _driverIds.AsReadOnly();
    public IReadOnlyList<DriverRating> DriverRatings => _driverRatings.AsReadOnly();

    private Rating(RaceWeekId raceWeekId, List<DriverId> driverIds, IDateProvider dateProvider)
        : base(RatingId.Create())
    {
        RaceWeekId = raceWeekId;
        IsSummarized = false;

        var date = dateProvider.Now.AddDays(_ratingExpiryDays);
        var ratingFinishDate = new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, new TimeSpan(1, 0, 0));

        Finish = ratingFinishDate;

        _driverIds = driverIds;
        _driverRatings = _driverIds.Select(e => DriverRating.Create(e)).ToList();
    }

    public static Rating Create(RaceWeekId raceWeekId, List<DriverId> driverIds, IDateProvider dateProvider)
        => new(raceWeekId, driverIds, dateProvider);

    public void AddRatings(ICollection<UserDriverRating> ratings)
    {
        var driverIds = ratings.Select(e => e.DriverId).ToList();

        if (driverIds.Count != _driverIds.Count && driverIds.All(_driverIds.Contains))
        {
            throw new InvalidDriverRatingsException();
        }

        foreach (var rating in ratings)
        {
            var driverRating = _driverRatings.First(e => e.DriverId == rating.DriverId);

            driverRating.AddRate(rating.Rating);
        }
    }

    public void SummerizeDriversRatings(ICollection<UserDriverRating> ratings)
    {
        var driverIds = ratings.Select(e => e.DriverId)
            .Distinct()
            .ToList();

        if (driverIds.Count > 0 && driverIds.Count != _driverIds.Count && driverIds.All(_driverIds.Contains))
        {
            throw new InvalidDriverRatingsException();
        }

        var groups = ratings.GroupBy(e => e.DriverId)
            .Select(e => new { DriverId = e.Key, Items = e.ToList() })
            .ToList();

        foreach (var group in groups)
        {
            var driverRating = _driverRatings.First(e => e.DriverId == group.DriverId);

            driverRating.AddAllRatings(group.Items);
        }

        IsSummarized = true;
    }

    public bool IsReadyToSummerize(IDateProvider dateProvider)
    {
        return Finish < dateProvider.UtcNow;
    }

#pragma warning disable CS8618
    private Rating() : base(RatingId.Create()) { }
#pragma warning restore CS8618
}
