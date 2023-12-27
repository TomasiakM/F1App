using Application.Dtos.RaceWeek.Responses;
using Application.Interfaces;
using Domain.Aggregates.Seasons.ValueObjects;
using MapsterMapper;
using MediatR;

namespace Application.Features.RaceWeeks.Queries.GetBySeason;
internal sealed class GetBySeasonRaceWeeksQueryHandler : IRequestHandler<GetBySeasonRaceWeeksQuery, List<RaceWeekResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBySeasonRaceWeeksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<RaceWeekResponse>> Handle(GetBySeasonRaceWeeksQuery request, CancellationToken cancellationToken)
    {
        var seasonId = SeasonId.Create(request.SeasonId);
        var season = await _unitOfWork.Seasons.GetAsync(seasonId);

        var raceWeeks = await _unitOfWork.RaceWeeks.GetBySeasonAsync(seasonId, cancellationToken);


        var trackIds = raceWeeks.Select(e => e.TrackId).ToList();
        var tracks = await _unitOfWork.Tracks.FindAllAsync(e => trackIds.Contains(e.Id));

        var raceWeekDtos = raceWeeks.Select(e =>
            _mapper.Map<RaceWeekResponse>((e, season, tracks.First(track => track.Id == e.TrackId))));

        return raceWeekDtos.ToList();
    }
}
