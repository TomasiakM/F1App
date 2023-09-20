using Domain.Aggregates.Seasons;
using Domain.Aggregates.Seasons.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class SeasonRepository : GenericRepository<Season, SeasonId>, ISeasonRepository
{
    public SeasonRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
