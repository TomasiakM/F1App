using Domain.Aggregates.Teams;
using Domain.Aggregates.Teams.ValueObjects;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal sealed class TeamRepository : GenericRepository<Team, TeamId>, ITeamRepository
{
    public TeamRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<(List<Team>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _set
            .OrderByDescending(e => e.Name)
            .AsQueryable();

        var teams = await query.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pages = await query.GetPageCountAsync(pageSize);

        return (teams, pages);
    }
}
