using Application.Dtos.RaceWeek.Responses;
using Application.Interfaces;
using Domain.Aggregates.Tracks.ValueObjects;
using MapsterMapper;
using MediatR;

namespace Application.Features.RaceWeeks.Queries.GetByTrack;
internal sealed class GetByTrackRaceWeeksQueryHandler : IRequestHandler<GetByTrackRaceWeeksQuery, List<RaceWeekResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByTrackRaceWeeksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<RaceWeekResponse>> Handle(GetByTrackRaceWeeksQuery request, CancellationToken cancellationToken)
    {
        var trackId = TrackId.Create(request.TrackId);

        var raceWeeks = await _unitOfWork.RaceWeeks.FindAllAsync(e => e.TrackId == trackId);
        var track = await _unitOfWork.Tracks.FindAsync(e => e.Id == trackId);

        var seasonIds = raceWeeks.Select(e => e.SeasonId).ToList();
        var seasons = await _unitOfWork.Seasons.FindAllAsync(e => seasonIds.Contains(e.Id));

        var raceWeekDtos = raceWeeks.Select(e =>
            _mapper.Map<RaceWeekResponse>((e, seasons.First(s => s.Id == e.SeasonId), track)));

        return raceWeekDtos.ToList();
    }
}
