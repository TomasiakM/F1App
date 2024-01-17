using MediatR;

namespace Application.Features.Comments.Commands.Delete;
public record DeleteCommentCommand(
    Guid CommentId) : IRequest<Unit>;
