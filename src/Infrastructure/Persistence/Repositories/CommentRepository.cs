using Domain.Aggregates.Comments;
using Domain.Aggregates.Comments.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class CommentRepository : GenericRepository<Comment, CommentId>, ICommentRepository
{
    public CommentRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
