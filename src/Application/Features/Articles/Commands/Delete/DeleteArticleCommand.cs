using MediatR;

namespace Application.Features.Articles.Commands.Delete;
public record DeleteArticleCommand(
    Guid ArticleId) : IRequest<Unit>;
