using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
public abstract class FreePracticeResult : SessionResult
{
    protected FreePracticeResult(int place, int laps, TimeSpan? fastestLap, DriverId driverId, TeamId teamId)
        : base(place, laps, fastestLap, driverId, teamId)
    {
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return base.GetEqualityComponents();
    }
}
