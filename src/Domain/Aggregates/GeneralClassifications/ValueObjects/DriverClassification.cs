using Domain.Aggregates.Drivers.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.GeneralClassifications.ValueObjects;
public sealed class DriverClassification : ValueObject
{
    public int Place { get; private set; }
    public float Points { get; private set; }

    public DriverId DriverId { get; private set; }

    private DriverClassification(int place, float points, DriverId driverId)
    {
        Place = place;
        Points = points;
        DriverId = driverId;
    }

    public static DriverClassification Create(int place, float points, DriverId driverId)
        => new(place, points, driverId);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Place;
        yield return Points;
        yield return DriverId;
    }

    #pragma warning disable CS8618
    private DriverClassification() { }
    #pragma warning restore CS8618
}
