using Application.Dtos.RaceWeek.Responses;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.RaceWeeks.Queries.GetNext;
internal sealed class GetNextRaceWeekQueryHandler : IRequestHandler<GetNextRaceWeekQuery, RaceWeekResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;
    private readonly IMapper _mapper;

    public GetNextRaceWeekQueryHandler(IUnitOfWork unitOfWork, IDateProvider dateProvider, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
        _mapper = mapper;
    }

    public async Task<RaceWeekResponse> Handle(GetNextRaceWeekQuery request, CancellationToken cancellationToken)
    {
        var raceWeek = await _unitOfWork.RaceWeeks.GetNextAsync(_dateProvider, cancellationToken);

        if(raceWeek is null)
        {
            throw new NotFoundException();
        }

        var track = await _unitOfWork.Tracks.GetAsync(raceWeek.TrackId, cancellationToken);
        var season = await _unitOfWork.Seasons.GetAsync(raceWeek.SeasonId, cancellationToken);

        var raceWeekDto = _mapper.Map<RaceWeekResponse>((raceWeek, season, track));

        return raceWeekDto;
    }
}
