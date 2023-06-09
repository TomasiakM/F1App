using MediatR;

namespace Application.Features.Articles.Commands.Update;
public record UpdateArticleCommand(
    Guid ArticleId,
    string Title,
    string Image,
    string Description,
    string DescriptionHtml,
    string PublishedAt,
    List<string> TagIds) : IRequest<Unit>;
