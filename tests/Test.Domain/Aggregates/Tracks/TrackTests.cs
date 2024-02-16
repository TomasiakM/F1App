using Domain.Aggregates.Tracks;

namespace Test.Domain.Aggregates.Tracks;
public class TrackTests
{
    [Fact]
    public void Track_Create_ShouldCreateTrack()
    {
        var name = "name";
        var countryCode = "POL";
        var image = "image";
        var descriptionHtml = "description";
        var length = 2332;
        var corners = 12;
        var city = "city";

        var track = Track.Create(name, countryCode, image, descriptionHtml, length, corners, city);

        Assert.Equal(name, track.Name);
        Assert.Equal(countryCode, track.CountryCode);
        Assert.Equal(image, track.Image);
        Assert.Equal(descriptionHtml, track.DescriptionHtml);
        Assert.Equal(length, track.Length);
        Assert.Equal(corners, track.Corners);
        Assert.Equal(city, track.City);
    }

    [Fact]
    public void Track_Update_ShouldUpdateTrack()
    {
        var name = "name";
        var countryCode = "POL";
        var image = "image";
        var descriptionHtml = "description";
        var length = 2332;
        var corners = 12;
        var city = "city";

        var track = Track.Create(name, countryCode, image, descriptionHtml, length, corners, city);

        var name2 = "name2";
        var countryCode2 = "GER";
        var image2 = "image2";
        var descriptionHtml2 = "description2";
        var length2 = 23323;
        var corners2 = 123;
        var city2 = "city2";

        track.Update(name2, countryCode2, image2, descriptionHtml2, length2, corners2, city2);

        Assert.Equal(name2, track.Name);
        Assert.Equal(countryCode2, track.CountryCode);
        Assert.Equal(image2, track.Image);
        Assert.Equal(descriptionHtml2, track.DescriptionHtml);
        Assert.Equal(length2, track.Length);
        Assert.Equal(corners2, track.Corners);
        Assert.Equal(city2, track.City);
    }
}
