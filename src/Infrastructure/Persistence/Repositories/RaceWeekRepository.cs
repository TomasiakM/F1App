using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal sealed class RaceWeekRepository : GenericRepository<RaceWeek, RaceWeekId>, IRaceWeekRepository
{
    public RaceWeekRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
