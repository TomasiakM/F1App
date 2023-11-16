using Domain.Aggregates.Drivers.ValueObjects;
using Domain.DDD;
using Domain.Extensions;

namespace Domain.Aggregates.Drivers;
public sealed class Driver : AggregateRoot<DriverId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string CountryCode { get; private set; }
    public string Image { get; private set; }
    public string DescriptionHtml { get; private set; }
    public string Slug { get; private set; }

    private Driver(string firstName, string lastName, DateTime dateOfBirth, string countryCode, string image, string descriptionHtml)
        : base(DriverId.Create())
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        CountryCode = countryCode;
        Image = image;
        DescriptionHtml = descriptionHtml;

        Slug = GenerateSlug();
    }

    public static Driver Create(string firstName, string lastName, DateTime dateOfBirth, string countryCode, string image, string descriptionHtml)
        => new(firstName, lastName, dateOfBirth, countryCode, image, descriptionHtml);

    public void Update(string firstName, string lastName, DateTime dateOfBirth, string countryCode, string image, string descriptionHtml)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        CountryCode = countryCode;
        Image = image;
        DescriptionHtml = descriptionHtml;

        Slug = GenerateSlug();
    }

    private string GenerateSlug()
    {
        var s = FirstName + " - " + LastName;

        return s.ToUrlFriendly();
    }

    #pragma warning disable CS8618
    private Driver() : base(DriverId.Create()) { }
    #pragma warning restore CS8618
}