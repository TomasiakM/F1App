using Domain.Aggregates.DriverStatistics.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.DriverStatistics;
public interface IDriverStatisticRepository : IRepository<DriverStatistic, DriverStatisticId>
{
}
