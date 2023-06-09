using Domain.Aggregates.Articles.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Articles;
public interface IArticleRepository : IRepository<Article, ArticleId>
{
    Task<List<Article>> GetPagginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<int> GetPageCountAsync(int pageSize, CancellationToken cancellationToken = default);
}
