using Application.Dtos.Common;
using Application.Dtos.Driver.Responses;
using MediatR;

namespace Application.Features.Drivers.Queries.GetPaginated;
public record GetPaginatedDriversQuery(
    PaginationFilters Filters) : IRequest<Pagination<DriverResponse>>;
