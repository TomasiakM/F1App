using Application.Features.RaceWeeks.Commands.UpdateRaceQualificationSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateRaceSessionResults;
using Application.Interfaces;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.TeamStatistics;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.TeamStatistics.Events;
internal sealed class UpdateTeamStatisticsEventHandler : INotificationHandler<RaceSessionResultsUpdatedEvent>, INotificationHandler<RaceQualificationSessionResultsEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTeamStatisticsEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RaceSessionResultsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await UpdateTeamStatistics(RaceWeekId.Create(notification.RaceWeekId), cancellationToken);
    }

    public async Task Handle(RaceQualificationSessionResultsEvent notification, CancellationToken cancellationToken)
    {
        await UpdateTeamStatistics(RaceWeekId.Create(notification.RaceWeekId), cancellationToken);
    }

    private async Task UpdateTeamStatistics(RaceWeekId raceWeekId, CancellationToken cancellationToken)
    {
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);

        if (raceWeek is null || raceWeek.Race is null)
        {
            throw new NotFoundException();
        }

        var teamIds = raceWeek.Race.SessionResults.Select(e => e.TeamId).ToList();
        var teamStatistics = await _unitOfWork.TeamStatistics.FindAllAsync(e => teamIds.Contains(e.TeamId));

        var raceWeeks = await _unitOfWork.RaceWeeks.GetAllAsync();

        foreach (var teamId in teamIds)
        {
            var statistic = teamStatistics.FirstOrDefault(e => e.TeamId == teamId);

            if (statistic is null)
            {
                statistic = TeamStatistic.Create(teamId);
                _unitOfWork.TeamStatistics.Add(statistic);
            }

            statistic.UpdateStatistics(raceWeeks);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
