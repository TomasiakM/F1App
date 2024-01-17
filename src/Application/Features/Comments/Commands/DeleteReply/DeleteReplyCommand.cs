using MediatR;

namespace Application.Features.Comments.Commands.DeleteReply;
public record DeleteReplyCommand(
    Guid CommentId,
    Guid ReplyId) : IRequest<Unit>;
