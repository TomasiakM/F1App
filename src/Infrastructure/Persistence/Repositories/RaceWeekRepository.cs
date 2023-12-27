using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal sealed class RaceWeekRepository : GenericRepository<RaceWeek, RaceWeekId>, IRaceWeekRepository
{
    public RaceWeekRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<RaceWeek?> GetNextAsync(IDateProvider dateProvider, CancellationToken cancellationToken = default)
    {
        return await _set
            .Where(e => e.Race!.Start > dateProvider.UtcNow)
            .OrderByDescending(e => e.Race!.Start)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<RaceWeek>> GetBySeasonAsync(SeasonId seasonId, CancellationToken cancellationToken = default)
    {
        return await _set
            .Where(e => e.SeasonId == seasonId)
            .OrderByDescending(e => e.Race!.Start)
            .OrderByDescending(e => e.Sprint!.Start)
            .OrderByDescending(e => e.RaceQualifications!.Start)
            .OrderByDescending(e => e.SprintQualifications!.Start)
            .OrderByDescending(e => e.FP3!.Start)
            .OrderByDescending(e => e.FP2!.Start)
            .OrderByDescending(e => e.FP1!.Start)
            .ToListAsync();
    }
}
