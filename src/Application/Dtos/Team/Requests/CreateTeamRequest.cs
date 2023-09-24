namespace Application.Dtos.Team.Requests;
public record CreateTeamRequest(
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml);
