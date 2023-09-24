using MediatR;

namespace Application.Features.Teams.Commands.Update;
public record UpdateTeamCommand(
    Guid TeamId,
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml) : IRequest<Unit>;
