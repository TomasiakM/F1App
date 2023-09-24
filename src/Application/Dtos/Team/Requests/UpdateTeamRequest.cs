namespace Application.Dtos.Team.Requests;
public record UpdateTeamRequest(
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml);
