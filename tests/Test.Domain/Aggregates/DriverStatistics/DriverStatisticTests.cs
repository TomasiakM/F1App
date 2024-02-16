using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.DriverStatistics;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.Enums;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Aggregates.Tracks.ValueObjects;

namespace Test.Domain.Aggregates.DriverStatistics;
public class DriverStatisticTests
{
    [Fact]
    public void DriverStatistic_Create_ShouldCreateDriverStatistic()
    {
        var driverId = DriverId.Create();

        var driverStatistic = DriverStatistic.Create(driverId);

        Assert.Equal(driverId, driverStatistic.DriverId);
        Assert.Equal(0, driverStatistic.Podiums);
        Assert.Equal(0, driverStatistic.Poles);
        Assert.Equal(0, driverStatistic.Wins);
        Assert.Equal(0, driverStatistic.Races);
    }

    [Fact]
    public void DriverStatistic_UpdateStatistics_ShouldUpdateStatistics()
    {
        var driverId = DriverId.Create();

        var driverStatistic = DriverStatistic.Create(driverId);

        var season = SeasonId.Create();
        var trackId1 = TrackId.Create();

        var driverId2 = DriverId.Create();

        var raceWeek1 = RaceWeek.Create("test", trackId1, season);
        raceWeek1.CreateSprintSession(new(2022, 2, 2, 12, 0, 0, TimeSpan.Zero));

        var teamId = TeamId.Create();
        raceWeek1.Sprint!.SetSessionResults(new() {
            SprintResult.Create(1, 3, TimeSpan.Zero, FinishType.Finished, 1, TimeSpan.Zero, 3, driverId, teamId),
            SprintResult.Create(2, 3, TimeSpan.Zero, FinishType.Finished, 2, TimeSpan.Zero, 1, driverId2, teamId)
        });

        raceWeek1.CreateRaceQualificationSession(new(2022, 2, 2, 15, 0, 0, TimeSpan.Zero));
        raceWeek1.RaceQualifications!.SetSessionResults(new() {
            RaceQualificationResult.Create(1, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, driverId, teamId),
            RaceQualificationResult.Create(2, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, driverId2, teamId)
        });

        raceWeek1.CreateRaceSession(new(2022, 2, 3, 15, 0, 0, TimeSpan.Zero));
        raceWeek1.Race!.SetSessionResults(new() {
            RaceResult.Create(1, 3, TimeSpan.Zero, FinishType.Finished, 1, TimeSpan.Zero, 9, driverId, teamId),
            RaceResult.Create(2, 3, TimeSpan.Zero, FinishType.Finished, 2, TimeSpan.Zero, 6, driverId2, teamId)
        });

        var trackId2 = TrackId.Create();
        var raceWeek2 = RaceWeek.Create("test2", trackId2, season);
        raceWeek2.CreateSprintSession(new(2022, 3, 2, 12, 0, 0, TimeSpan.Zero));

        raceWeek2.Sprint!.SetSessionResults(new() {
            SprintResult.Create(1, 3, TimeSpan.Zero, FinishType.Finished, 1, TimeSpan.Zero, 6, driverId2, teamId),
            SprintResult.Create(2, 3, TimeSpan.Zero, FinishType.Finished, 2, TimeSpan.Zero, 5, driverId, teamId)
        });

        raceWeek2.CreateRaceQualificationSession(new(2022, 3, 2, 15, 0, 0, TimeSpan.Zero));
        raceWeek2.RaceQualifications!.SetSessionResults(new() {
            RaceQualificationResult.Create(1, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, driverId2, teamId),
            RaceQualificationResult.Create(2, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, driverId, teamId)
        });

        raceWeek2.CreateRaceSession(new(2022, 3, 3, 15, 0, 0, TimeSpan.Zero));
        raceWeek2.Race!.SetSessionResults(new() {
            RaceResult.Create(1, 3, TimeSpan.Zero, FinishType.Finished, 1, TimeSpan.Zero, 12, driverId2, teamId),
            RaceResult.Create(2, 3, TimeSpan.Zero, FinishType.Finished, 2, TimeSpan.Zero, 5, driverId, teamId)
        });

        driverStatistic.UpdateStatistics(new List<RaceWeek> { raceWeek1, raceWeek2 });

        Assert.Equal(driverId, driverStatistic.DriverId);
        Assert.Equal(2, driverStatistic.Podiums);
        Assert.Equal(1, driverStatistic.Poles);
        Assert.Equal(1, driverStatistic.Wins);
        Assert.Equal(2, driverStatistic.Races);
    }
}
