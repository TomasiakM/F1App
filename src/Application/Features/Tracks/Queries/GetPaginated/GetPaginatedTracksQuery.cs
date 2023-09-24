using Application.Dtos.Common;
using Application.Dtos.Track.Responses;
using MediatR;

namespace Application.Features.Tracks.Queries.GetPaginated;
public record GetPaginatedTracksQuery(
    PaginationFilters Filters) : IRequest<Pagination<TrackResponse>>;
