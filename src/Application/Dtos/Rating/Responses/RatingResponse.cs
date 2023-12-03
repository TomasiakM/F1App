using Application.Dtos.Driver.Responses;
using Application.Dtos.RaceWeek.Responses;

namespace Application.Dtos.Rating.Responses;
public record RatingResponse(
    Guid Id,
    DateTimeOffset Finish,
    RaceWeekResponse RaceWeek,
    List<DriverResponse> Drivers);

public record DriverRatingResponse(
    double Rating,
    DriverResponse Driver);