using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Tags.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Articles;
public interface IArticleRepository : IRepository<Article, ArticleId>
{
    Task<(List<Article>, int)> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<(List<Article>, int)> GetPaginatedAdminAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<(List<Article>, int)> GetPaginatedByTagIdAsync(TagId tagId, int page, int pageSize, CancellationToken cancellationToken = default);
}
