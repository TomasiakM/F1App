using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Aggregates.TeamStatistics.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.TeamStatistics;
public sealed class TeamStatistic : AggregateRoot<TeamStatisticId>
{
    public TeamId TeamId { get; private set; }
    public int Poles { get; private set; }
    public int Wins { get; private set; }
    public int Podiums { get; private set; }

    private TeamStatistic(TeamId teamId)
        : base(TeamStatisticId.Create())
    {
        TeamId = teamId;
    }

    public void UpdateStatistics(ICollection<RaceWeek> raceWeeks)
    {
        var racesWithTeam = raceWeeks
            .Where(e => e.Race is not null && e.Race.SessionResults.Any(sr => sr.TeamId == TeamId))
            .Select(e => e.Race);

        var qualificationsWithTeam = raceWeeks
            .Where(e => e.RaceQualifications is not null && e.RaceQualifications.SessionResults.Any(sr => sr.TeamId == TeamId))
            .Select(e => e.RaceQualifications);

        Podiums = racesWithTeam.Where(e => e!.SessionResults.Any(sr => sr.Place <= 3)).Count();
        Wins = racesWithTeam.Where(e => e!.SessionResults.Any(sr => sr.Place == 1)).Count();

        Poles = qualificationsWithTeam.Where(e => e!.SessionResults.Any(sr => sr.Place == 1)).Count();
    }

    public static TeamStatistic Create(TeamId teamId) =>
        new(teamId);

#pragma warning disable CS8618
    private TeamStatistic() : base(TeamStatisticId.Create()) { }
#pragma warning restore CS8618
}
