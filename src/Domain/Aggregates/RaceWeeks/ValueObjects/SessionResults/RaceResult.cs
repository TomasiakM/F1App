using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.Enums;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
using Domain.Aggregates.Teams.ValueObjects;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
public sealed class RaceResult : SessionResult
{
    public int StartPosition { get; private set; }
    public TimeSpan? FinishTime { get; private set; }
    public FinishType FinishType { get; private set; }
    public float Points { get; private set; }

    private RaceResult(int place, int laps, TimeSpan? fastestLap, FinishType finishType, int startPosition, TimeSpan? finishTime, float points, DriverId driverId, TeamId teamId)
        : base(place, laps, fastestLap, driverId, teamId)
    {
        StartPosition = startPosition;
        FinishTime = finishTime;
        Points = points;
        FinishType = finishType;
    }

    public static RaceResult Create(int place, int laps, TimeSpan? fastestLap, FinishType finishType, int startPosition, TimeSpan? finishTime, float points, DriverId driverId, TeamId teamId)
        => new(place, laps, fastestLap, finishType, startPosition, finishTime, points, driverId, teamId);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return base.GetEqualityComponents();
        yield return StartPosition;
        yield return FinishTime;
        yield return FinishType;
        yield return Points;
    }

    private RaceResult() : base(0, 0, TimeSpan.Zero, DriverId.Create(), TeamId.Create()) { }
}
