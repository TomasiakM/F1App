using Application.Dtos.Track.Responses;
using MediatR;

namespace Application.Features.Tracks.Queries.GetAll;
public record GetAllTracksQuery() : IRequest<List<TrackResponse>>;
