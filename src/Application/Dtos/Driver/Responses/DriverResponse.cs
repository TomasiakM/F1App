namespace Application.Dtos.Driver.Responses;
public record DriverResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Slug,
    DateTime DateOfBirth,
    string Image,
    string CountryCode,
    string DescriptionHtml);
