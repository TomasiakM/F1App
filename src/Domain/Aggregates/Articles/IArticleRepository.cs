using Domain.Aggregates.Articles.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Articles;
public interface IArticleRepository : IRepository<Article, ArticleId>
{
}
