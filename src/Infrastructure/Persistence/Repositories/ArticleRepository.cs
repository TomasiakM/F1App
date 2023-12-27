using Domain.Aggregates.Articles;
using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Tags.ValueObjects;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal class ArticleRepository : GenericRepository<Article, ArticleId>, IArticleRepository
{
    public ArticleRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<(List<Article>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _set
            .Where(e => e.PublishedAt < DateTimeOffset.UtcNow)
            .OrderByDescending(e => e.PublishedAt)
            .AsQueryable();

        var articles = await query.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pages = await query.GetPageCountAsync(pageSize);

        return (articles, pages);
    }

    public async Task<(List<Article>, int)> GetPaginatedAdminAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _set
            .OrderByDescending(e => e.PublishedAt)
            .AsQueryable();

        var articles = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pages = await query.GetPageCountAsync(pageSize);

        return (articles, pages);
    }

    public async Task<(List<Article>, int)> GetPaginatedByTagIdAsync(TagId tagId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _set
            .Where(e => e.TagIds.Any(e => e.Value == tagId.Value))
            .Where(e => e.PublishedAt < DateTimeOffset.UtcNow)
            .OrderByDescending(e => e.PublishedAt)
            .AsQueryable();

        var articles = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pages = await query.GetPageCountAsync(pageSize);

        return (articles, pages);
    }
}
