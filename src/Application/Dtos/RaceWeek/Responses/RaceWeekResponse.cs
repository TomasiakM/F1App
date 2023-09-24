using Application.Dtos.Season.Responses;
using Application.Dtos.Track.Responses;

namespace Application.Dtos.RaceWeek.Responses;
public record RaceWeekResponse(
    Guid Id,
    string Name,
    string Slug,
    TrackResponse Track,
    SeasonResponse Season,
    DateTimeOffset? FP1,
    DateTimeOffset? FP2,
    DateTimeOffset? FP3,
    DateTimeOffset? SprintQualification,
    DateTimeOffset? Sprint,
    DateTimeOffset? RaceQualification,
    DateTimeOffset? Race);
