using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.GeneralClassifications;
using Domain.Aggregates.GeneralClassifications.ValueObjects;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;

namespace Test.Domain.Aggregates.GeneralClassifications;
public class GeneralClassificationTests
{
    [Fact]
    public void GeneralClassification_Create_ShouldCreateGeneralClassification()
    {
        var seasonId = SeasonId.Create();

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverClassifications = new List<DriverClassification> {
            DriverClassification.Create(1, 4, driverId1),
            DriverClassification.Create(2, 3, driverId2)
        };

        var teamId1 = TeamId.Create();
        var teamId2 = TeamId.Create();

        var teamClassifications = new List<TeamClassification> {
            TeamClassification.Create(1, 4, teamId1),
            TeamClassification.Create(2, 3, teamId2)
        };

        var generalClassification = GeneralClassification.Create(seasonId, driverClassifications, teamClassifications);

        Assert.Equal(seasonId, generalClassification.SeasonId);
        Assert.Equal(2, generalClassification.Drivers.Count);
        Assert.Equal(2, generalClassification.Teams.Count);
    }

    [Fact]
    public void GeneralClassification_SetDriverClassification_ShouldSetDriverClassification()
    {
        var seasonId = SeasonId.Create();

        var generalClassification = GeneralClassification.Create(seasonId, new(), new());

        var driverId1 = DriverId.Create();
        var driverId2 = DriverId.Create();

        var driverClassifications = new List<DriverClassification> {
            DriverClassification.Create(1, 4, driverId1),
            DriverClassification.Create(2, 3, driverId2)
        };

        generalClassification.SetDriverClassification(driverClassifications);

        Assert.Equal(seasonId, generalClassification.SeasonId);
        Assert.Equal(2, generalClassification.Drivers.Count);
        Assert.Equal(0, generalClassification.Teams.Count);
    }

    [Fact]
    public void GeneralClassification_SetTeamClassification_ShouldSetDriverClassification()
    {
        var seasonId = SeasonId.Create();

        var generalClassification = GeneralClassification.Create(seasonId, new(), new());

        var teamId1 = TeamId.Create();
        var teamId2 = TeamId.Create();

        var teamClassifications = new List<TeamClassification> {
            TeamClassification.Create(1, 4, teamId1),
            TeamClassification.Create(2, 3, teamId2)
        };

        generalClassification.SetTeamClassification(teamClassifications);

        Assert.Equal(seasonId, generalClassification.SeasonId);
        Assert.Equal(0, generalClassification.Drivers.Count);
        Assert.Equal(2, generalClassification.Teams.Count);
    }
}
