namespace Application.Dtos.Rating.Responses;
public record RatingSummaryResponse(
    Guid Id,
    Guid RaceWeekId,
    List<DriverRatingResponse> Drivers);
