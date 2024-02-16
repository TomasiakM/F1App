using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.UserDriverRatings;
using Domain.DDD;

namespace Domain.Aggregates.Ratings.Entities;
public sealed class DriverRating : Entity<int>
{
    public DriverId DriverId { get; private set; }
    public int RatesCount { get; private set; }
    public double Rating { get; private set; }

    private DriverRating(DriverId driverId)
        : base(0)
    {
        DriverId = driverId;
        RatesCount = 0;
        Rating = 0;
    }

    public static DriverRating Create(DriverId driverId)
        => new(driverId);

    public void AddRate(double rating)
    {
        if (RatesCount == 0)
        {
            Rating = rating;
        }
        else
        {
            Rating = ((Rating * RatesCount) + rating) / (RatesCount + 1);
        }

        RatesCount++;
    }

    public void AddAllRatings(ICollection<UserDriverRating> ratings)
    {
        RatesCount = ratings.Count;
        Rating = ratings.Average(e => e.Rating);
    }

#pragma warning disable CS8618
    private DriverRating() : base(0) { }
#pragma warning restore CS8618
}