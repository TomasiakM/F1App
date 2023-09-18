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

    private Track(string name, string countryCode, string image, string descriptionHtml) 
        : base(TrackId.Create())
    {
        Name = name;
        CountryCode = countryCode;
        Image = image;
        DescriptionHtml = descriptionHtml;

        Slug = Name.ToUrlFriendly();
    }

    public static Track Create(string name, string countryCode, string image, string descriptionHtml)
        => new(name, countryCode, image, descriptionHtml);

    public void Update(string name, string countryCode, string image, string descriptionHtml)
    {
        Name = name;
        CountryCode = countryCode;
        Image = image;
        DescriptionHtml = descriptionHtml;

        Slug = Name.ToUrlFriendly();
    }
}
