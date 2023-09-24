using MediatR;

namespace Application.Features.Tracks.Commands.Update;
public record UpdateTrackCommand(
    Guid TrackId,
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml) : IRequest<Unit>;
