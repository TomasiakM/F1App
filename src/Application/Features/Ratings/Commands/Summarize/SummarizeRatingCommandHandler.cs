using Application.Interfaces;
using Domain.Aggregates.Ratings.Exceptions;
using Domain.Aggregates.Ratings.ValueObjects;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Ratings.Commands.Summarize;
internal sealed class SummarizeRatingCommandHandler : IRequestHandler<SummarizeRatingCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;

    public SummarizeRatingCommandHandler(IUnitOfWork unitOfWork, IDateProvider dateProvider)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
    }

    public async Task<Unit> Handle(SummarizeRatingCommand request, CancellationToken cancellationToken)
    {
        var ratingId = RatingId.Create(request.RatingId);
        var rating = await _unitOfWork.Ratings.GetAsync(ratingId, cancellationToken);

        if (rating is null)
        {
            throw new NotFoundException();
        }

        if (!rating.IsReadyToSummerize(_dateProvider))
        {
            throw new RatingIsNotFinishedException();
        }

        var driverRatings = await _unitOfWork.UserDriverRatings.FindAllAsync(e => e.RatingId == rating.Id);

        rating.SummerizeDriversRatings(driverRatings);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
