namespace Application.Dtos.Driver.Requests;
public record UpdateDriverRequest(
    string FirstName,
    string LastName,
    string DateOfBirth,
    string Image,
    string CountryCode,
    string DescriptionHtml);
