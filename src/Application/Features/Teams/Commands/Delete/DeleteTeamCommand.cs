using MediatR;

namespace Application.Features.Teams.Commands.Delete;
public record DeleteTeamCommand(
    Guid TeamId) : IRequest<Unit>;
