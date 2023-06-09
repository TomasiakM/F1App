using MediatR;

namespace Application.Features.Articles.Commands.Like;
public record LikeArticleCommand(
    Guid ArticleId) : IRequest<Unit>;
