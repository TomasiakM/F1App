using Application.Dtos.Common;
using Application.Dtos.Team.Responses;
using MediatR;

namespace Application.Features.Teams.Queries.GetPaginated;
public record GetPaginatedTeamsQuery(
    PaginationFilters Filters) : IRequest<Pagination<TeamResponse>>;
