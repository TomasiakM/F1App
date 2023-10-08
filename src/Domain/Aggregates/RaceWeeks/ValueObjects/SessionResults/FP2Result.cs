using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
using Domain.Aggregates.Teams.ValueObjects;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
public sealed class FP2Result : FreePracticeResult
{
    private FP2Result(int place, int laps, TimeSpan? fastestLap, DriverId driverId, TeamId teamId)
        : base(place, laps, fastestLap, driverId, teamId)
    {
    }

    public static FP2Result Create(int place, int laps, TimeSpan? fastestLap, DriverId driverId, TeamId teamId)
        => new(place, laps, fastestLap, driverId, teamId);

    private FP2Result() : base(0, 0, TimeSpan.Zero, DriverId.Create(), TeamId.Create()) { }
}
