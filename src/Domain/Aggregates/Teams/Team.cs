using Domain.Aggregates.Teams.ValueObjects;
using Domain.DDD;
using Domain.Extensions;

namespace Domain.Aggregates.Teams;
public sealed class Team : AggregateRoot<TeamId>
{
    public string Name { get; private set; }
    public string Image { get; private set; }
    public string CountryCode { get; private set; }
    public string DescriptionHtml { get; private set; }
    public string Slug { get; private set; }

    private Team(string name, string image, string countryCode, string descriptionHtml) 
        : base(TeamId.Create())
    {
        Name = name;
        Image = image;
        CountryCode = countryCode;
        DescriptionHtml = descriptionHtml;

        Slug = Name.ToUrlFriendly();
    }

    public static Team Create(string name, string image, string countryCode, string descriptionHtml)
        => new(name, image, countryCode, descriptionHtml);

    public void Update(string name, string image, string countryCode, string descriptionHtml)
    {
        Name = name;
        Image = image;
        CountryCode = countryCode;
        DescriptionHtml = descriptionHtml;

        Slug = Name.ToUrlFriendly();
    }
}
