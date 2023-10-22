using Domain.Aggregates.GeneralClassifications.ValueObjects;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.GeneralClassifications;
public sealed class GeneralClassification : AggregateRoot<GeneralClassificationId>
{
    public SeasonId SeasonId { get; private set; }

    private List<DriverClassification> _drivers = new();
    public IReadOnlyList<DriverClassification> Drivers => _drivers.AsReadOnly();

    private List<TeamClassification> _teams = new();
    public IReadOnlyList<TeamClassification> Teams => _teams.AsReadOnly();

    private GeneralClassification(SeasonId seasonId, List<DriverClassification> drivers, List<TeamClassification> teams) 
        : base(GeneralClassificationId.Create())
    {
        SeasonId = seasonId;
        _drivers = drivers;
        _teams = teams;
    }

    public static GeneralClassification Create(SeasonId seasonId, List<DriverClassification> drivers, List<TeamClassification> teams)
        => new(seasonId, drivers, teams);

    public void SetDriverClassification(List<DriverClassification> driverResults)
    {
        _drivers = driverResults;
    }

    public void SetTeamClassification(List<TeamClassification> teamResults)
    {
        _teams = teamResults;
    }

#pragma warning disable CS8618
    private GeneralClassification() : base(GeneralClassificationId.Create()) { }
    #pragma warning restore CS8618
}
