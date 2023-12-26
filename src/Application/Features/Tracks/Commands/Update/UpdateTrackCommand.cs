using MediatR;

namespace Application.Features.Tracks.Commands.Update;
public record UpdateTrackCommand(
    Guid TrackId,
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml,
    int Length,
    int Corners,
    string City) : IRequest<Unit>;
