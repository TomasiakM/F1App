using Application.Dtos.Driver.Responses;
using Application.Dtos.RaceWeek.Responses;
using Application.Dtos.Rating.Responses;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Ratings.Queries.GetActive;
internal sealed class GetActiveRatingQueryHandler : IRequestHandler<GetActiveRatingQuery, RatingResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;
    private readonly IMapper _mapper;

    public GetActiveRatingQueryHandler(IUnitOfWork unitOfWork, IDateProvider dateProvider, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
        _mapper = mapper;
    }

    public async Task<RatingResponse> Handle(GetActiveRatingQuery request, CancellationToken cancellationToken)
    {
        var rating = await _unitOfWork.Ratings.FindAsync(e => e.Finish > _dateProvider.UtcNow);

        if(rating is null)
        {
            throw new NotFoundException();
        }

        var drivers = await _unitOfWork.Drivers.FindAllAsync(e => rating.DriverIds.Contains(e.Id));
        var driverDtos = _mapper.Map<List<DriverResponse>>(drivers);

        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(rating.RaceWeekId);
        var season = await _unitOfWork.Seasons.GetAsync(raceWeek!.SeasonId);
        var track = await _unitOfWork.Tracks.GetAsync(raceWeek!.TrackId);
        
        var raceWeekDto = _mapper.Map<RaceWeekResponse>((raceWeek, season, track));

        return new(rating.Id.Value, rating.Finish, raceWeekDto, driverDtos);
    }
}
