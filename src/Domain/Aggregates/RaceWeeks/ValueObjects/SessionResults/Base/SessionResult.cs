using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
public abstract class SessionResult : ValueObject
{
    public int Place { get; private set; }
    public int Laps { get; private set; }
    public TimeSpan? FastestLap { get; private set; }
    public DriverId DriverId { get; private set; }
    public TeamId TeamId { get; private set; }

    protected SessionResult(int place, int laps, TimeSpan? fastestLap, DriverId driverId, TeamId teamId)
    {
        Place = place;
        Laps = laps;
        FastestLap = fastestLap;
        DriverId = driverId;
        TeamId = teamId;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Place;
        yield return Laps;
        yield return FastestLap;
        yield return DriverId;
        yield return TeamId;
    }
}
