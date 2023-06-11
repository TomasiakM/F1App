using MediatR;

namespace Application.Features.Comments.Commands.Create;
public record CreateCommentCommand(
    Guid ArticleId,
    string Text) : IRequest<Unit>;
