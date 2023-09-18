using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.Enums;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
public sealed class FP3Result : FreePracticeResult
{
    public FP3Result(int place, int laps, TimeSpan fastestLap, FinishType finishType, DriverId driverId)
        : base(place, laps, fastestLap, finishType, driverId)
    {
    }
}
