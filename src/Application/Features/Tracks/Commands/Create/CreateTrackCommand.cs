using MediatR;

namespace Application.Features.Tracks.Commands.Create;
public record CreateTrackCommand(
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml) : IRequest<Unit>;
