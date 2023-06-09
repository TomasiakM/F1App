using Application.Dtos.Article.Responses;
using MediatR;

namespace Application.Features.Articles.Queries.GetArticle;
public record GetArticleQuery(
    string Slug) : IRequest<ArticleResponse>;
