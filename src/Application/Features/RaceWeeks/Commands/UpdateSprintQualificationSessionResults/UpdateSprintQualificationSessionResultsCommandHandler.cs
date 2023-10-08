using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateSprintQualificationSessionResults;
internal sealed class UpdateSprintQualificationSessionResultsCommandHandler : IRequestHandler<UpdateSprintQualificationSessionResultsCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSprintQualificationSessionResultsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateSprintQualificationSessionResultsCommand request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);

        if(raceWeek is null || raceWeek.SprintQualifications is null)
        {
            throw new NotFoundException();
        }

        var sessionResults = request.SessionResults.Select(e =>
            SprintQualificationResult.Create(
                e.Place,
                string.IsNullOrEmpty(e.Q1Time) ? null : TimeSpan.Parse(e.Q1Time),
                string.IsNullOrEmpty(e.Q2Time) ? null : TimeSpan.Parse(e.Q2Time),
                string.IsNullOrEmpty(e.Q3Time) ? null : TimeSpan.Parse(e.Q3Time),
                DriverId.Create(new(e.DriverId)),
                TeamId.Create(new(e.TeamId)))
            ).ToList();

        raceWeek.SprintQualifications.SetSessionResults(sessionResults);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
