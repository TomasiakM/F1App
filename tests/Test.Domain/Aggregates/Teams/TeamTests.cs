using Domain.Aggregates.Teams;

namespace Test.Domain.Aggregates.Teams;
public class TeamTests
{
    [Fact]
    public void Team_Create_ShouldCreateTeam()
    {
        var name = "name";
        var image = "image";
        var countryCode = "POL";
        var descriptionHtml = "description";

        var team = Team.Create(name, image, countryCode, descriptionHtml);

        Assert.Equal(name, team.Name);
        Assert.Equal(image, team.Image);
        Assert.Equal(countryCode, team.CountryCode);
        Assert.Equal(descriptionHtml, team.DescriptionHtml);
    }

    [Fact]
    public void Team_Update_ShouldUpdateTeam()
    {
        var name = "name";
        var image = "image";
        var countryCode = "POL";
        var descriptionHtml = "description";

        var team = Team.Create(name, image, countryCode, descriptionHtml);

        var name2 = "name2";
        var image2 = "image2";
        var countryCode2 = "GER";
        var descriptionHtml2 = "description2";

        team.Update(name2, image2, countryCode2, descriptionHtml2);

        Assert.Equal(name2, team.Name);
        Assert.Equal(image2, team.Image);
        Assert.Equal(countryCode2, team.CountryCode);
        Assert.Equal(descriptionHtml2, team.DescriptionHtml);
    }
}
