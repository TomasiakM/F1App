using Domain.Aggregates.DriverStatistics;
using Domain.Aggregates.DriverStatistics.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class DriverStatisticRepository : GenericRepository<DriverStatistic, DriverStatisticId>, IDriverStatisticRepository
{
    public DriverStatisticRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
