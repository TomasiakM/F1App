using Application.Features.RaceWeeks.Commands.UpdateRaceQualificationSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateRaceSessionResults;
using Application.Interfaces;
using Domain.Aggregates.DriverStatistics;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.DriverStatistics.Events;
internal sealed class UpdateDriversStatisticsEventHandler : INotificationHandler<RaceSessionResultsUpdatedEvent>, INotificationHandler<RaceQualificationSessionResultsEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDriversStatisticsEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RaceSessionResultsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await UpdateDriverStatistics(RaceWeekId.Create(notification.RaceWeekId), cancellationToken);
    }

    public async Task Handle(RaceQualificationSessionResultsEvent notification, CancellationToken cancellationToken)
    {
        await UpdateDriverStatistics(RaceWeekId.Create(notification.RaceWeekId), cancellationToken);
    }

    private async Task UpdateDriverStatistics(RaceWeekId raceWeekId, CancellationToken cancellationToken)
    {
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);

        if (raceWeek is null || raceWeek.Race is null)
        {
            throw new NotFoundException();
        }

        var driverIds = raceWeek.Race.SessionResults.Select(e => e.DriverId).ToList();
        var driverStatistics = await _unitOfWork.DriverStatistics.FindAllAsync(e => driverIds.Contains(e.DriverId));

        var raceWeeks = await _unitOfWork.RaceWeeks.GetAllAsync(cancellationToken);
        foreach (var driverId in driverIds)
        {
            var statistic = driverStatistics.FirstOrDefault(e => e.DriverId == driverId);

            if (statistic is null)
            {
                statistic = DriverStatistic.Create(driverId);
                _unitOfWork.DriverStatistics.Add(statistic);
            }

            statistic.UpdateStatistics(raceWeeks);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
