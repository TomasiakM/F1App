using Domain.Aggregates.Seasons;

namespace Test.Domain.Aggregates.Seasons;
public class SeasonTests
{
    [Fact]
    public void Season_Create_ShouldCreateSeason()
    {
        var season = Season.Create(2022);

        Assert.Equal(2022, season.Year);
    }
}
