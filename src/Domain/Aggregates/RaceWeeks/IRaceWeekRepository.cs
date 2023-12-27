using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.RaceWeeks;
public interface IRaceWeekRepository : IRepository<RaceWeek, RaceWeekId>
{
    Task<RaceWeek?> GetNextAsync(IDateProvider dateProvider, CancellationToken cancellationToken);
    Task<List<RaceWeek>> GetBySeasonAsync(SeasonId seasonId, CancellationToken cancellationToken);
}
