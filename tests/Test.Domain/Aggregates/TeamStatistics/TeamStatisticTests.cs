using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.Enums;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Aggregates.TeamStatistics;
using Domain.Aggregates.Tracks.ValueObjects;

namespace Test.Domain.Aggregates.TeamStatistics;
public class TeamStatisticTests
{
    [Fact]
    public void TeamStatistic_Create_ShouldCreateTeamStatistic()
    {
        var teamId = TeamId.Create();

        var teamStatistic = TeamStatistic.Create(teamId);

        Assert.Equal(teamId, teamStatistic.TeamId);
        Assert.Equal(0, teamStatistic.Podiums);
        Assert.Equal(0, teamStatistic.Poles);
        Assert.Equal(0, teamStatistic.Wins);
    }

    [Fact]
    public void TeamStatistic_UpdateStatistics_ShouldUpdateTeamStatistics()
    {
        var teamId1 = TeamId.Create();
        var teamId2 = TeamId.Create();

        var teamStatistic = TeamStatistic.Create(teamId1);

        var season = SeasonId.Create();
        var trackId1 = TrackId.Create();

        var driverId2 = DriverId.Create();

        var raceWeek1 = RaceWeek.Create("test", trackId1, season);
        raceWeek1.CreateSprintSession(new(2022, 2, 2, 12, 0, 0, TimeSpan.Zero));

        var driverId = DriverId.Create();
        raceWeek1.Sprint!.SetSessionResults(new() {
            SprintResult.Create(1, 3, TimeSpan.Zero, FinishType.Finished, 1, TimeSpan.Zero, 3, driverId, teamId1),
            SprintResult.Create(2, 3, TimeSpan.Zero, FinishType.Finished, 2, TimeSpan.Zero, 1, driverId2, teamId2)
        });

        raceWeek1.CreateRaceQualificationSession(new(2022, 2, 2, 15, 0, 0, TimeSpan.Zero));
        raceWeek1.RaceQualifications!.SetSessionResults(new() {
            RaceQualificationResult.Create(1, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, driverId, teamId1),
            RaceQualificationResult.Create(2, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, driverId2, teamId2)
        });

        raceWeek1.CreateRaceSession(new(2022, 2, 3, 15, 0, 0, TimeSpan.Zero));
        raceWeek1.Race!.SetSessionResults(new() {
            RaceResult.Create(1, 3, TimeSpan.Zero, FinishType.Finished, 1, TimeSpan.Zero, 9, driverId, teamId1),
            RaceResult.Create(2, 3, TimeSpan.Zero, FinishType.Finished, 2, TimeSpan.Zero, 6, driverId2, teamId2)
        });

        var trackId2 = TrackId.Create();
        var raceWeek2 = RaceWeek.Create("test2", trackId2, season);
        raceWeek2.CreateSprintSession(new(2022, 3, 2, 12, 0, 0, TimeSpan.Zero));

        raceWeek2.Sprint!.SetSessionResults(new() {
            SprintResult.Create(1, 3, TimeSpan.Zero, FinishType.Finished, 1, TimeSpan.Zero, 6, driverId2, teamId2),
            SprintResult.Create(2, 3, TimeSpan.Zero, FinishType.Finished, 2, TimeSpan.Zero, 5, driverId, teamId1)
        });

        raceWeek2.CreateRaceQualificationSession(new(2022, 3, 2, 15, 0, 0, TimeSpan.Zero));
        raceWeek2.RaceQualifications!.SetSessionResults(new() {
            RaceQualificationResult.Create(1, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, driverId2, teamId2),
            RaceQualificationResult.Create(2, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, driverId, teamId1)
        });

        raceWeek2.CreateRaceSession(new(2022, 3, 3, 15, 0, 0, TimeSpan.Zero));
        raceWeek2.Race!.SetSessionResults(new() {
            RaceResult.Create(1, 3, TimeSpan.Zero, FinishType.Finished, 1, TimeSpan.Zero, 12, driverId2, teamId2),
            RaceResult.Create(2, 3, TimeSpan.Zero, FinishType.Finished, 2, TimeSpan.Zero, 5, driverId, teamId1)
        });

        teamStatistic.UpdateStatistics(new List<RaceWeek> { raceWeek1, raceWeek2 });

        Assert.Equal(teamId1, teamStatistic.TeamId);
        Assert.Equal(2, teamStatistic.Podiums);
        Assert.Equal(1, teamStatistic.Poles);
        Assert.Equal(1, teamStatistic.Wins);
    }
}
