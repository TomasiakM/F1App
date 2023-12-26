using MediatR;

namespace Application.Features.Tracks.Commands.Create;
public record CreateTrackCommand(
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml,
    int Length,
    int Corners,
    string City) : IRequest<Unit>;
