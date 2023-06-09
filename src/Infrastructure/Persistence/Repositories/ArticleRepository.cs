using Domain.Aggregates.Articles;
using Domain.Aggregates.Articles.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal class ArticleRepository : GenericRepository<Article, ArticleId>, IArticleRepository
{
    public ArticleRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }

    public Task<List<Article>> GetPagginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return _set
            .Where(e => e.PublishedAt > DateTimeOffset.UtcNow)
            .OrderBy(e => e.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetPageCountAsync(int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await  _set
            .Where(e => e.PublishedAt > DateTimeOffset.UtcNow)
            .CountAsync();
        
        var pages = (int)Math.Ceiling(count / (double)pageSize);

        return pages;
    }
}
