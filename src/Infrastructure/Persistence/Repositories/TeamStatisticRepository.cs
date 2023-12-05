using Domain.Aggregates.TeamStatistics;
using Domain.Aggregates.TeamStatistics.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class TeamStatisticRepository : GenericRepository<TeamStatistic, TeamStatisticId>, ITeamStatisticRepository
{
    public TeamStatisticRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
