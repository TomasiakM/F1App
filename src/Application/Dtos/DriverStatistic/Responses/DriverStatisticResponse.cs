namespace Application.Dtos.DriverStatistic.Responses;
public record DriverStatisticResponse(
    Guid DriverId,
    int Races,
    int Poles,
    int Wins,
    int Podiums);
