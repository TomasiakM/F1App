namespace Application.Dtos.Rating.Responses;
public record UserDriverRatingsResponse(
    Guid UserId,
    List<UserDriverRatingResponse> Ratings);

public record UserDriverRatingResponse(
    Guid DriverId,
    int Rating);
