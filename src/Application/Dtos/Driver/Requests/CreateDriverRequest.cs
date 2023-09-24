namespace Application.Dtos.Driver.Requests;
public record CreateDriverRequest(
    string FirstName,
    string LastName,
    string DateOfBirth,
    string Image,
    string CountryCode,
    string DescriptionHtml);
