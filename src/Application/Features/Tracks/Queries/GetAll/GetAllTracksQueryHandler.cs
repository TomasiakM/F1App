using Application.Dtos.Track.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Tracks.Queries.GetAll;
internal sealed class GetAllTracksQueryHandler : IRequestHandler<GetAllTracksQuery, List<TrackResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllTracksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TrackResponse>> Handle(GetAllTracksQuery request, CancellationToken cancellationToken)
    {
        var traks = await _unitOfWork.Tracks.GetAllAsync();
        var trackDtos = _mapper.Map<List<TrackResponse>>(traks);

        return trackDtos;
    }
}
