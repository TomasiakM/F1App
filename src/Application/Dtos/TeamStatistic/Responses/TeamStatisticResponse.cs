namespace Application.Dtos.TeamStatistic.Responses;
public record TeamStatisticResponse(
    Guid TeamId,
    int Poles,
    int Wins,
    int Podiums);
