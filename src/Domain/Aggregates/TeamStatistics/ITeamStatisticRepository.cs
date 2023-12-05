using Domain.Aggregates.TeamStatistics.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.TeamStatistics;
public interface ITeamStatisticRepository : IRepository<TeamStatistic, TeamStatisticId>
{
}
