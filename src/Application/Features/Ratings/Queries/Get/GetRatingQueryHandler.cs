using Application.Dtos.Driver.Responses;
using Application.Dtos.Rating.Responses;
using Application.Interfaces;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.Ratings.Queries.Get;
internal sealed class GetRatingQueryHandler : IRequestHandler<GetRatingQuery, RatingSummaryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRatingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RatingSummaryResponse> Handle(GetRatingQuery request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);

        var rating = await _unitOfWork.Ratings.FindAsync(e => e.RaceWeekId == raceWeekId);

        if(rating is null || !rating.IsSummarized)
        {
            throw new NotFoundException();
        }

        var drivers = await _unitOfWork.Drivers.FindAllAsync(e => rating.DriverIds.Contains(e.Id));
        var driverDtos = _mapper.Map<List<DriverResponse>>(drivers);

        var driverRatings = rating.DriverRatings.Select(
            e => new DriverRatingResponse(
                e.Rating, 
                _mapper.Map<DriverResponse>(drivers.First(d => d.Id == e.DriverId))))
            .ToList();

        return new RatingSummaryResponse(
            rating.Id.Value,
            rating.RaceWeekId.Value,
            driverRatings);
    }
}
