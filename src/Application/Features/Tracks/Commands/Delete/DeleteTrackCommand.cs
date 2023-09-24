using MediatR;

namespace Application.Features.Tracks.Commands.Delete;
public record DeleteTrackCommand(
    Guid TrackId) : IRequest<Unit>;
