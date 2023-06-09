using Application.Dtos.Article.Responses;
using MediatR;

namespace Application.Features.Articles.Commands.Create;
public record CreateArticleCommand(
    string Title,
    string Image,
    string Description,
    string DescriptionHtml,
    string PublishedAt,
    List<string> TagIds) : IRequest<ArticleResponse>;
