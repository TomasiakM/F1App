using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateFP2SessionResults;
internal sealed class UpdateFP2SessionResultsCommandHandler : IRequestHandler<UpdateFP2SessionResultsCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFP2SessionResultsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateFP2SessionResultsCommand request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);

        if(raceWeek is null || raceWeek.FP2 is null)
        {
            throw new NotFoundException();
        }

        var sessionResults = request.SessionResults.Select(
            e => FP2Result.Create(
                e.Place,
                e.Laps,
                TimeSpan.Parse(e.FastestLap),
                DriverId.Create(new(e.DriverId)),
                TeamId.Create(new(e.TeamId)))
            ).ToList();

        raceWeek.FP2.SetSessionResults(sessionResults);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
