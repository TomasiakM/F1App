using Domain.Aggregates.Teams.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.GeneralClassifications.ValueObjects;
public sealed class TeamClassification : ValueObject
{
    public int Place { get; private set; }
    public float Points { get; private set; }

    public TeamId TeamId { get; private set; }

    private TeamClassification(int place, float points, TeamId driverId)
    {
        Place = place;
        Points = points;
        TeamId = driverId;
    }

    public static TeamClassification Create(int place, float points, TeamId teamId)
        => new(place, points, teamId);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Place;
        yield return Points;
        yield return TeamId;
    }

    #pragma warning disable CS8618
    private TeamClassification() { }
    #pragma warning restore CS8618
}
