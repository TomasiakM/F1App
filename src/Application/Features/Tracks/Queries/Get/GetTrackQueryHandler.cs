using Application.Dtos.Track.Responses;
using Application.Interfaces;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.Tracks.Queries.Get;
internal sealed class GetTrackQueryHandler : IRequestHandler<GetTrackQuery, TrackResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTrackQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TrackResponse> Handle(GetTrackQuery request, CancellationToken cancellationToken)
    {
        var track = await _unitOfWork.Tracks.FindAsync(e => e.Slug == request.Slug, cancellationToken);

        if(track is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<TrackResponse>(track);
    }
}
