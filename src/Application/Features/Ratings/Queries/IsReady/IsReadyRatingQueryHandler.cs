using Application.Dtos.Rating.Responses;
using Application.Interfaces;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Ratings.Queries.IsReadyToStart;
internal class IsReadyRatingQueryHandler : IRequestHandler<IsReadyRatingQuery, RatingIsReadyResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;

    public IsReadyRatingQueryHandler(IUnitOfWork unitOfWork, IDateProvider dateProvider)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
    }

    public async Task<RatingIsReadyResponse> Handle(IsReadyRatingQuery request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var rating = await _unitOfWork.Ratings.FindAsync(e => e.RaceWeekId == raceWeekId);

        var isCreated = false;
        var isReadyToCreate = false;
        var isSummarized = false;
        var isReadyToSummarize = false;
        Guid? ratingId = null;

        if (rating is not null)
        {
            ratingId = rating.Id.Value;
            isCreated = true;

            if (rating.IsSummarized)
            {
                isSummarized = true;
            }
            else if (rating.IsReadyToSummerize(_dateProvider))
            {
                isReadyToSummarize = true;
            }

            return new(isCreated, isReadyToCreate, isSummarized, isReadyToSummarize, ratingId);
        }

        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId) ?? throw new NotFoundException();

        if (raceWeek.IsReadyToStartRating(_dateProvider))
        {
            isReadyToCreate = true;
        }

        return new(isCreated, isReadyToCreate, isSummarized, isReadyToSummarize, ratingId);
    }
}
