using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
using Domain.Aggregates.Teams.ValueObjects;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
public sealed class FP1Result : FreePracticeResult
{
    private FP1Result(int place, int laps, TimeSpan? fastestLap, DriverId driverId, TeamId teamId)
        : base(place, laps, fastestLap, driverId, teamId)
    {
    }

    public static FP1Result Create(int place, int laps, TimeSpan? fastestLap, DriverId driverId, TeamId teamId)
        => new(place, laps, fastestLap, driverId, teamId);

    private FP1Result() : base(0, 0, TimeSpan.Zero, DriverId.Create(), TeamId.Create()) { }
}
