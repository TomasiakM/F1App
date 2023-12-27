using Application.Dtos.Article.Responses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Articles.Queries.GetPaginatedByTag;
public record GetPaginatedArticlesByTagQuery(
    string TagSlug,
    PaginationFilters Filters) : IRequest<Pagination<ArticleResponse>>
{
}
