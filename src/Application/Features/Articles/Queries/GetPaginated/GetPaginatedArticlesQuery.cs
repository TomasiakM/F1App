using Application.Dtos.Article.Responses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Articles.Queries.GetPaginated;
public record GetPaginatedArticlesQuery(
    PaginationFilters Filters) : IRequest<Pagination<ArticleResponse>>;
