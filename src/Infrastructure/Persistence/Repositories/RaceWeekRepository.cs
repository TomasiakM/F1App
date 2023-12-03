using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.ValueObjects;
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
}
