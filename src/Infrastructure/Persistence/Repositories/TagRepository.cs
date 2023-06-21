using Domain.Aggregates.Tags;
using Domain.Aggregates.Tags.ValueObjects;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal sealed class TagRepository : GenericRepository<Tag, TagId>, ITagRepository
{
    public TagRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<(List<Tag>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _set.AsQueryable();

        var tags = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pages = await query.GetPageCountAsync(pageSize);

        return (tags, pages);
    }
}
