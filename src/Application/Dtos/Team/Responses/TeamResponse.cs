namespace Application.Dtos.Team.Responses;
public record TeamResponse(
    Guid Id,
    string Name,
    string Slug,
    string Image,
    string CountryCode,
    string DescriptionHtml);
