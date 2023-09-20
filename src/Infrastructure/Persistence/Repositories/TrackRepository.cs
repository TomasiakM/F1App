using Domain.Aggregates.Tracks;
using Domain.Aggregates.Tracks.ValueObjects;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal sealed class TrackRepository : GenericRepository<Track, TrackId>, ITrackRepository
{
    public TrackRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<(List<Track>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _set
            .OrderByDescending(e => e.Name)
            .AsQueryable();

        var tracks = await query.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pages = await query.GetPageCountAsync(pageSize);

        return (tracks, pages);
    }
}
