using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Drivers;
public interface IDriverRepository : IRepository<Driver, DriverId>
{
    Task<(List<Driver>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}
