using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
using Domain.Aggregates.Teams.ValueObjects;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
public sealed class SprintQualificationResult : QualificationResult
{
    private SprintQualificationResult(int place, TimeSpan? q1Time, TimeSpan? q2Time, TimeSpan? q3Time, DriverId driverId, TeamId teamId)
        : base(place, q1Time, q2Time, q3Time, driverId, teamId)
    {
    }

    public static SprintQualificationResult Create(int place, TimeSpan? q1Time, TimeSpan? q2Time, TimeSpan? q3Time, DriverId driverId, TeamId teamId)
        => new(place, q1Time, q2Time, q3Time, driverId, teamId);

    private SprintQualificationResult() : base(0, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, DriverId.Create(), TeamId.Create()) { }
}
