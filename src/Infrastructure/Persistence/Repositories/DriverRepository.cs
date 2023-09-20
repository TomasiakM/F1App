using Domain.Aggregates.Drivers;
using Domain.Aggregates.Drivers.ValueObjects;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal sealed class DriverRepository : GenericRepository<Driver, DriverId>, IDriverRepository
{
    public DriverRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<(List<Driver>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _set
            .OrderByDescending(s => s.FirstName)
            .OrderByDescending (s => s.LastName)
            .AsQueryable();

        var drivers = await query.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pages = await query.GetPageCountAsync(pageSize);

        return (drivers, pages);
    }
}
