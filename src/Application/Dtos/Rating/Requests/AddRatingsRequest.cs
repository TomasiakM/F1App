namespace Application.Dtos.Rating.Requests;
public record AddRatingsRequest(
    List<DriverRatingRequest> Ratings);

public record DriverRatingRequest(
    string DriverId,
    int Rating);
