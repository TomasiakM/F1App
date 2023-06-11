using MediatR;

namespace Application.Features.Comments.Commands.AddReply;
public record AddReplyCommand(
    Guid CommentId,
    string Text) : IRequest<Unit>;
