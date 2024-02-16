using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Tracks.ValueObjects;
using Domain.Extensions;

namespace Test.Domain.Aggregates.RaceWeeks;
public class RaceWeekTests
{
    [Fact]
    public void RaceWeek_Create_ShouldCreateRaceWeek()
    {
        var name = "name";
        var trackId = TrackId.Create();
        var seasonId = SeasonId.Create();

        var raceWek = RaceWeek.Create(name, trackId, seasonId);

        Assert.Equal(name, raceWek.Name);
        Assert.Equal(name.ToUrlFriendly(), raceWek.Slug);
        Assert.Equal(trackId, raceWek.TrackId);
        Assert.Equal(seasonId, raceWek.SeasonId);

        Assert.Null(raceWek.FP1);
        Assert.Null(raceWek.FP2);
        Assert.Null(raceWek.FP3);
        Assert.Null(raceWek.SprintQualifications);
        Assert.Null(raceWek.RaceQualifications);
        Assert.Null(raceWek.Sprint);
        Assert.Null(raceWek.Race);
    }

    [Fact]
    public void RaceWeek_Update_ShouldUpdateRaceWeek()
    {
        var name = "name";
        var trackId = TrackId.Create();
        var seasonId = SeasonId.Create();

        var raceWek = RaceWeek.Create(name, trackId, seasonId);

        var name2 = "name2";
        var trackId2 = TrackId.Create();

        raceWek.Update(name2, trackId2);

        Assert.Equal(name2, raceWek.Name);
        Assert.Equal(name2.ToUrlFriendly(), raceWek.Slug);
        Assert.Equal(trackId2, raceWek.TrackId);
        Assert.Equal(seasonId, raceWek.SeasonId);

        Assert.Null(raceWek.FP1);
        Assert.Null(raceWek.FP2);
        Assert.Null(raceWek.FP3);
        Assert.Null(raceWek.SprintQualifications);
        Assert.Null(raceWek.RaceQualifications);
        Assert.Null(raceWek.Sprint);
        Assert.Null(raceWek.Race);
    }

    [Fact]
    public void RaceWeek_CreateFp1Session_ShouldCreateFp1Session()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateFp1Session(date);

        Assert.NotNull(raceWek.FP1);
        Assert.Equal(date, raceWek.FP1.Start);
    }

    [Fact]
    public void RaceWeek_UpdateFp1Session_ShouldUpdateFp1Session()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateFp1Session(date);

        var date2 = new DateTimeOffset(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.UpdateFp1Session(date2);

        Assert.NotNull(raceWek.FP1);
        Assert.Equal(date2, raceWek.FP1.Start);
    }

    [Fact]
    public void RaceWeek_CreateFp2Session_ShouldCreateFp2Session()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateFp2Session(date);

        Assert.NotNull(raceWek.FP2);
        Assert.Equal(date, raceWek.FP2.Start);
    }

    [Fact]
    public void RaceWeek_UpdateFp2Session_ShouldUpdateFp2Session()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateFp2Session(date);

        var date2 = new DateTimeOffset(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.UpdateFp2Session(date2);

        Assert.NotNull(raceWek.FP2);
        Assert.Equal(date2, raceWek.FP2.Start);
    }

    [Fact]
    public void RaceWeek_CreateFp3Session_ShouldCreateFp3Session()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateFp3Session(date);

        Assert.NotNull(raceWek.FP3);
        Assert.Equal(date, raceWek.FP3.Start);
    }

    [Fact]
    public void RaceWeek_UpdateFp3Session_ShouldUpdateFp3Session()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateFp3Session(date);

        var date2 = new DateTimeOffset(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.UpdateFp3Session(date2);

        Assert.NotNull(raceWek.FP3);
        Assert.Equal(date2, raceWek.FP3.Start);
    }

    [Fact]
    public void RaceWeek_CreateSprintQualificationSession_ShouldCreateSprintQualificationSession()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateSprintQualificationSession(date);

        Assert.NotNull(raceWek.SprintQualifications);
        Assert.Equal(date, raceWek.SprintQualifications.Start);
    }

    [Fact]
    public void RaceWeek_UpdateSprintQualificationSession_ShouldUpdateSprintQualificationSession()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateSprintQualificationSession(date);

        var date2 = new DateTimeOffset(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.UpdateSprintQualificationSession(date2);

        Assert.NotNull(raceWek.SprintQualifications);
        Assert.Equal(date2, raceWek.SprintQualifications.Start);
    }

    [Fact]
    public void RaceWeek_CreateSprintSession_ShouldCreateSprintSession()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateSprintSession(date);

        Assert.NotNull(raceWek.Sprint);
        Assert.Equal(date, raceWek.Sprint.Start);
    }

    [Fact]
    public void RaceWeek_UpdateSprintSession_ShouldUpdateSprintSession()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateSprintSession(date);

        var date2 = new DateTimeOffset(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.UpdateSprintSession(date2);

        Assert.NotNull(raceWek.Sprint);
        Assert.Equal(date2, raceWek.Sprint.Start);
    }

    [Fact]
    public void RaceWeek_CreateRaceQualificationSession_ShouldCreateRaceQualificationSession()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateRaceQualificationSession(date);

        Assert.NotNull(raceWek.RaceQualifications);
        Assert.Equal(date, raceWek.RaceQualifications.Start);
    }

    [Fact]
    public void RaceWeek_UpdateRaceQualificationSession_ShouldUpdateRaceQualificationSession()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateRaceQualificationSession(date);

        var date2 = new DateTimeOffset(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.UpdateRaceQualificationSession(date2);

        Assert.NotNull(raceWek.RaceQualifications);
        Assert.Equal(date2, raceWek.RaceQualifications.Start);
    }

    [Fact]
    public void RaceWeek_CreateRaceSession_ShouldCreateRaceSession()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateRaceSession(date);

        Assert.NotNull(raceWek.Race);
        Assert.Equal(date, raceWek.Race.Start);
    }

    [Fact]
    public void RaceWeek_UpdateRaceSession_ShouldUpdateRaceSession()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        var date = new DateTimeOffset(2022, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.CreateRaceSession(date);

        var date2 = new DateTimeOffset(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
        raceWek.UpdateRaceSession(date2);

        Assert.NotNull(raceWek.Race);
        Assert.Equal(date2, raceWek.Race.Start);
    }

    [Fact]
    public void RaceWeek_GetAllSprintResults_ShouldReturnList()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        Assert.Empty(raceWek.GetAllSprintResults());
    }

    [Fact]
    public void RaceWeek_GetAllRaceResults_ShouldReturnList()
    {
        var raceWek = RaceWeek.Create("name", TrackId.Create(), SeasonId.Create());

        Assert.Empty(raceWek.GetAllRaceResults());
    }
}
