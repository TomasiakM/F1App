using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.Enums;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
public sealed class SprintResult : SessionResult
{
    public int StartPosition { get; private set; }
    public TimeSpan FinishTime { get; private set; }
    public float Points { get; private set; }

    private SprintResult(int place, int laps, TimeSpan fastestLap, FinishType finishType, int startPosition, TimeSpan finishTime, float points, DriverId driverId)
        : base(place, laps, fastestLap, finishType, driverId)
    {
        StartPosition = startPosition;
        FinishTime = finishTime;
        Points = points;
    }

    public static SprintResult Create(int place, int laps, TimeSpan fastestLap, FinishType finishType, int startPosition, TimeSpan finishTime, float points, DriverId driverId)
        => new(place, laps, fastestLap, finishType, startPosition, finishTime, points, driverId);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return base.GetEqualityComponents();
        yield return StartPosition;
        yield return FinishTime;
        yield return Points;
    }
}
