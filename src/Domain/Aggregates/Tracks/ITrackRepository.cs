using Domain.Aggregates.Tracks.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Tracks;
public interface ITrackRepository : IRepository<Track, TrackId>
{
    Task<(List<Track>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}
