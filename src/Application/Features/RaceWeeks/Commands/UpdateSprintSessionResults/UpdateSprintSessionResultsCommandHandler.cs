using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.Enums;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateSprintSessionResults;
internal sealed class UpdateSprintSessionResultsCommandHandler : IRequestHandler<UpdateSprintSessionResultsCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSprintSessionResultsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateSprintSessionResultsCommand request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);

        if(raceWeek is null || raceWeek.Sprint is null)
        {
            throw new NotFoundException();
        }

        var sessionResults = request.SessionResults.Select(e =>
            SprintResult.Create(
                e.Place,
                e.Laps,
                string.IsNullOrEmpty(e.FastestLap) ? null : TimeSpan.Parse(e.FastestLap),
                Enum.Parse<FinishType>(e.FinishType),
                e.StartPosition,
                string.IsNullOrEmpty(e.FinishTime) ? null : TimeSpan.Parse(e.FinishTime),
                e.Points,
                DriverId.Create(new(e.DriverId)),
                TeamId.Create(new(e.TeamId)))
            ).ToList();

        raceWeek.Sprint.SetSessionResults(sessionResults);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
