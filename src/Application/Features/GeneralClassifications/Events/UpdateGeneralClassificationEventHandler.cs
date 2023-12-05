using Application.Dtos.GeneralClassification.Common;
using Application.Features.RaceWeeks.Commands.UpdateRaceSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateSprintSessionResults;
using Application.Interfaces;
using Domain.Aggregates.GeneralClassifications;
using Domain.Aggregates.GeneralClassifications.ValueObjects;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Seasons.ValueObjects;
using MediatR;

namespace Application.Features.GeneralClassifications.Events;
internal sealed class UpdateGeneralClassificationEventHandler : INotificationHandler<RaceSessionResultsUpdatedEvent>, INotificationHandler<SprintSessionResultsUpdatedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGeneralClassificationEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SprintSessionResultsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await UpdateGeneralClassification(SeasonId.Create(notification.SeasonId), cancellationToken);
    }

    public async Task Handle(RaceSessionResultsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await UpdateGeneralClassification(SeasonId.Create(notification.SeasonId), cancellationToken);
    }

    private async Task UpdateGeneralClassification(SeasonId seasonId, CancellationToken cancellationToken = default)
    {
        var generalClassification = await _unitOfWork.GeneralClassifications.FindAsync(e => e.SeasonId == seasonId);
        var raceWeeks = await _unitOfWork.RaceWeeks.FindAllAsync(e => e.SeasonId == seasonId);

        var driverResults = GetDriverClassification(raceWeeks);
        var teamResults = GetTeamClassification(raceWeeks);

        if (generalClassification is null)
        {
            generalClassification = GeneralClassification.Create(seasonId, driverResults, teamResults);
            _unitOfWork.GeneralClassifications.Add(generalClassification);
        }
        else
        {
            generalClassification.SetDriverClassification(driverResults);
            generalClassification.SetTeamClassification(teamResults);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private List<RaceGeneralClassificationResult> GetAllRaceResults(ICollection<RaceWeek> raceWeeks)
    {
        var raceResults = raceWeeks.SelectMany(raceWeek =>
        {
            var list = new List<RaceGeneralClassificationResult>();

            var races = raceWeek.GetAllRaceResults()
                .Select(e => new RaceGeneralClassificationResult(e.DriverId, e.TeamId, e.Points, e.Place));

            var sprints = raceWeek.GetAllSprintResults()
                .Select(e => new RaceGeneralClassificationResult(e.DriverId, e.TeamId, e.Points, e.Place));

            list.AddRange(races);
            list.AddRange(sprints);

            return list;
        }).ToList();


        return raceResults;
    }

    private List<DriverClassification> GetDriverClassification(ICollection<RaceWeek> raceWeeks)
    {
        var results = GetAllRaceResults(raceWeeks);

        var drivers = results.GroupBy(driverResults => driverResults.DriverId)
            .Select(e =>
            {
                var positions = e.Select(e => e.Position).ToList();
                var points = e.Sum(e => e.Points);

                return new { e.Key, Positions = positions, Points = points };
            }).ToList();

        drivers.ForEach((e) => e.Positions.Sort());

        drivers.Sort((a, b) =>
        {
            if (a.Points != b.Points) return a.Points < b.Points ? 1 : -1;

            var maxA = a.Positions.Count - 1;
            var maxB = b.Positions.Count - 1;

            var maxLength = maxA > maxB ? maxA : maxB;
            for (var i = 0; i <= maxLength; i++)
            {
                if (i > maxA) return 1;
                if (i > maxB) return -1;

                if (a.Positions[i] < b.Positions[i]) return -1;
                else if (a.Positions[i] > b.Positions[i]) return 1;
            }

            return 0;
        });

        var driverClassificationList = drivers.Select((e, i) =>
        {
            return DriverClassification.Create(i + 1, e.Points, e.Key);
        }).ToList();

        return driverClassificationList;
    }

    private List<TeamClassification> GetTeamClassification(ICollection<RaceWeek> raceWeeks)
    {
        var results = GetAllRaceResults(raceWeeks);

        var teams = results.GroupBy(teamResults => teamResults.TeamId)
            .Select(e =>
            {
                var positions = e.Select(e => e.Position).ToList();
                var points = e.Sum(e => e.Points);

                return new { e.Key, Positions = positions, Points = points };
            }).ToList();

        teams.ForEach((e) => e.Positions.Sort());

        teams.Sort((a, b) =>
        {
            if (a.Points != b.Points) return a.Points < b.Points ? 1 : -1;

            var maxA = a.Positions.Count - 1;
            var maxB = b.Positions.Count - 1;

            var maxLength = maxA > maxB ? maxA : maxB;
            for (var i = 0; i <= maxLength; i++)
            {
                if (i > maxA) return 1;
                if (i > maxB) return -1;

                if (a.Positions[i] < b.Positions[i]) return -1;
                else if (a.Positions[i] > b.Positions[i]) return 1;
            }

            return 0;
        });

        var teamClassificationList = teams.Select((e, i) =>
        {
            return TeamClassification.Create(i + 1, e.Points, e.Key);
        });

        return teamClassificationList.ToList();
    }
}
