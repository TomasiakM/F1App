using Domain.Aggregates.Comments.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Comments;
public interface ICommentRepository : IRepository<Comment, CommentId>
{
}
