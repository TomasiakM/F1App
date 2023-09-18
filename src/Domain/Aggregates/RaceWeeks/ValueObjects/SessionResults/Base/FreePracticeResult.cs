using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.Enums;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
public abstract class FreePracticeResult : SessionResult
{
    protected FreePracticeResult(int place, int laps, TimeSpan fastestLap, FinishType finishType, DriverId driverId)
        : base(place, laps, fastestLap, finishType, driverId)
    {
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return base.GetEqualityComponents();
    }
}
