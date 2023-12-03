using Application.Interfaces;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.Ratings;
using Domain.Aggregates.Ratings.Exceptions;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Ratings.Commands.Create;
internal sealed class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;

    public CreateRatingCommandHandler(IUnitOfWork unitOfWork, IDateProvider dateProvider)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
    }

    public async Task<Unit> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        var pendingRating = await _unitOfWork.Ratings.FindAsync(e => _dateProvider.Now < e.Finish);
        if(pendingRating is not null)
        {
            throw new RatingIsCreatedException();
        }

        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var existingRating = await _unitOfWork.Ratings.FindAsync(e => e.RaceWeekId ==  raceWeekId);
        
        if(existingRating is not null)
        {
            throw new RatingForRaceWeekIsCreatedException();
        }
        
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);
        if (!IsValid(raceWeek, _dateProvider))
        {
            throw new NotFoundException();
        }

        var driverIds = raceWeek!.Race!.SessionResults.Select(e => e.DriverId).ToList();
        var rating = Rating.Create(raceWeekId, driverIds, _dateProvider);

        _unitOfWork.Ratings.Add(rating);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private static bool IsValid(RaceWeek? raceWeek, IDateProvider dateProvider)
    {
        return 
            raceWeek is not null && 
            raceWeek.Race is not null &&
            raceWeek.Race.Start < dateProvider.Now &&
            dateProvider.Now < raceWeek.Race.Start.AddDays(2) &&
            raceWeek.Race.SessionResults.Count != 0;
    }
}
