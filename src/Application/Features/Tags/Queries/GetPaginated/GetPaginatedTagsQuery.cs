using Application.Dtos.Common;
using Application.Dtos.Tag.Responses;
using MediatR;

namespace Application.Features.Tags.Queries.GetPaginated;
public record GetPaginatedTagsQuery(
    PaginationFilters Filters) : IRequest<Pagination<TagResponse>>;
