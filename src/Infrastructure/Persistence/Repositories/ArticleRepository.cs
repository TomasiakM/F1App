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
}
