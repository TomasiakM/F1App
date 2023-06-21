using Domain.Aggregates.Tags.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Tags;
public interface ITagRepository : IRepository<Tag, TagId>
{
    Task<(List<Tag>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}
