using Application.Dtos.Common;
using Application.Dtos.Track.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Tracks.Queries.GetPaginated;
internal sealed class GetPaginatedTracksQueryHandler : IRequestHandler<GetPaginatedTracksQuery, Pagination<TrackResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedTracksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<TrackResponse>> Handle(GetPaginatedTracksQuery request, CancellationToken cancellationToken)
    {
        var (tracks, totalPages) = await _unitOfWork.Tracks.GetPaginatedAsync(request.Filters.Page, request.Filters.PageSize, cancellationToken);

        var trackDtos = _mapper.Map<List<TrackResponse>>(tracks);

        return new(
            request.Filters.Page,
            request.Filters.PageSize,
            totalPages,
            trackDtos);
    }
}
