using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateFP3SessionResults;
internal sealed class UpdateFP3SessionResultsCommandHandler : IRequestHandler<UpdateFP3SessionResultsCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFP3SessionResultsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateFP3SessionResultsCommand request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);

        if (raceWeek is null || raceWeek.FP3 is null)
        {
            throw new NotFoundException();
        }

        var sessionResults = request.SessionResults.Select(
            e => FP3Result.Create(
                e.Place,
                e.Laps,
                TimeSpan.Parse(e.FastestLap),
                DriverId.Create(new(e.DriverId)),
                TeamId.Create(new(e.TeamId)))
            ).ToList();

        raceWeek.FP3.SetSessionResults(sessionResults);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
