using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.Enums;
using Domain.DDD;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
public abstract class SessionResult : ValueObject
{
    public int Place { get; private set; }
    public int Laps { get; private set; }
    public TimeSpan FastestLap { get; private set; }
    public FinishType FinishType { get; private set; }
    public DriverId DriverId { get; private set; }

    protected SessionResult(int place, int laps, TimeSpan fastestLap, FinishType finishType, DriverId driverId)
    {
        Place = place;
        Laps = laps;
        FastestLap = fastestLap;
        FinishType = finishType;
        DriverId = driverId;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Place;
        yield return Laps;
        yield return FastestLap;
        yield return FinishType;
        yield return DriverId;
    }
}
