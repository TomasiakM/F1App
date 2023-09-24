using MediatR;

namespace Application.Features.Seasons.Commands.Delete;
public record DeleteSeasonCommand(
    Guid SeasonId) : IRequest<Unit>;
