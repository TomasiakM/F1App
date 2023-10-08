using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateFP1SessionResults;
internal sealed class UpdateFP1SessionResultsCommandHandler : IRequestHandler<UpdateFP1SessionResultsCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFP1SessionResultsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateFP1SessionResultsCommand request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);

        if(raceWeek is null || raceWeek.FP1 is null)
        {
            throw new NotFoundException();
        }

        var sessionResults = request.SessionResults.Select(
            e => FP1Result.Create(
                e.Place, 
                e.Laps, 
                TimeSpan.Parse(e.FastestLap), 
                DriverId.Create(new(e.DriverId)), 
                TeamId.Create(new(e.TeamId)))
            ).ToList();

        raceWeek.FP1.SetSessionResults(sessionResults);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
