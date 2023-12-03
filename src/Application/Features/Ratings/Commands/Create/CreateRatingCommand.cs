using MediatR;

namespace Application.Features.Ratings.Commands.Create;
public record CreateRatingCommand(
    Guid RaceWeekId) : IRequest<Unit>;
