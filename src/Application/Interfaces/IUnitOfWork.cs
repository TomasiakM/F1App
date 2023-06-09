using Domain.Aggregates.Articles;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Tags;
using Domain.Aggregates.Users;

namespace Application.Interfaces;
public interface IUnitOfWork
{
    IArticleRepository Articles { get; }
    ICommentRepository Comments { get; }
    ITagRepository Tags { get; }
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
