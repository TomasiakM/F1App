using Application.Dtos.Track.Responses;
using MediatR;

namespace Application.Features.Tracks.Queries.Get;
public record GetTrackQuery(
    string Slug) : IRequest<TrackResponse>;
