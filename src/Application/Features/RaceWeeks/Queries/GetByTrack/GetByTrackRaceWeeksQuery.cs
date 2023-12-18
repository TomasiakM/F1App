using Application.Dtos.RaceWeek.Responses;
using MediatR;

namespace Application.Features.RaceWeeks.Queries.GetByTrack;
public record GetByTrackRaceWeeksQuery(
    Guid TrackId) : IRequest<List<RaceWeekResponse>>;
