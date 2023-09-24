using MediatR;

namespace Application.Features.Teams.Commands.Create;
public record CreateTeamCommand(
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml) : IRequest<Unit>;
