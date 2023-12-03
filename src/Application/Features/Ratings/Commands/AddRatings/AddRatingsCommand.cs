using MediatR;

namespace Application.Features.Ratings.Commands.AddRatings;
public record AddRatingsCommand(
    Guid RatingId,
    List<DriverRatingCommand> Ratings) : IRequest<Unit>;


public record DriverRatingCommand(
    string DriverId,
    int Rating);
