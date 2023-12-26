using Domain.Aggregates.Tracks.ValueObjects;
using Domain.DDD;
using Domain.Extensions;

namespace Domain.Aggregates.Tracks;
public sealed class Track : AggregateRoot<TrackId>
{
    public string Name { get; private set; }
    public string CountryCode { get; private set; }
    public string Image { get; private set; }
    public string DescriptionHtml { get; private set; }
    public string Slug { get; private set; }
    public int Length { get; private set; }
    public int Corners { get; private set; }
    public string City { get; private set; }

    private Track(string name, string countryCode, string image, string descriptionHtml, int length, int corners, string city)
        : base(TrackId.Create())
    {
        Name = name;
        CountryCode = countryCode;
        Image = image;
        DescriptionHtml = descriptionHtml;
        Length = length;
        Corners = corners;
        City = city;

        Slug = Name.ToUrlFriendly();
    }

    public static Track Create(string name, string countryCode, string image, string descriptionHtml, int length, int corners, string city)
        => new(name, countryCode, image, descriptionHtml, length, corners, city);

    public void Update(string name, string countryCode, string image, string descriptionHtml, int length, int corners, string city)
    {
        Name = name;
        CountryCode = countryCode;
        Image = image;
        DescriptionHtml = descriptionHtml;
        Length = length;
        Corners = corners;
        City = city;

        Slug = Name.ToUrlFriendly();
    }
}
