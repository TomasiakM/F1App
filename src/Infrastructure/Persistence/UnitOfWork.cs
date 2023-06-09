using Application.Interfaces;
using Domain.Aggregates.Articles;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Tags;
using Domain.Aggregates.Users;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public IArticleRepository Articles { get; }
    public ICommentRepository Comments { get; }
    public ITagRepository Tags { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        Articles = new ArticleRepository(_dbContext);
        Comments = new CommentRepository(_dbContext);
        Tags = new TagRepository(_dbContext);
        Users = new UserRepository(_dbContext);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
