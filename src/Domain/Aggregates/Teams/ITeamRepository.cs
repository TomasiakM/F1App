using Domain.Aggregates.Teams.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Teams;
public interface ITeamRepository : IRepository<Team, TeamId>
{
    Task<(List<Team>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}
