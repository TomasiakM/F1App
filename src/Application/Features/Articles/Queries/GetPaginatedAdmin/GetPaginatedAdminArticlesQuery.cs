using Application.Dtos.Article.Responses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Articles.Queries.GetPaginatedAdmin;
public record GetPaginatedAdminArticlesQuery(
    PaginationFilters Filters) : IRequest<Pagination<ArticleResponse>>;
