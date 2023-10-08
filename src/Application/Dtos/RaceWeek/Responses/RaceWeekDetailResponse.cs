using Application.Dtos.RaceWeek.Responses.Session;
using Application.Dtos.Season.Responses;
using Application.Dtos.Track.Responses;

namespace Application.Dtos.RaceWeek.Responses;
public record RaceWeekDetailResponse(
    Guid Id,
    string Name,
    string Slug,
    TrackResponse Track,
    SeasonResponse Season,
    SessionResponse<FreePracticeResultResponse>? FP1,
    SessionResponse<FreePracticeResultResponse>? FP2,
    SessionResponse<FreePracticeResultResponse>? FP3,
    SessionResponse<QualificationResultResponse>? SprintQualification,
    SessionResponse<RaceResultResponse>? Sprint,
    SessionResponse<QualificationResultResponse>? RaceQualification,
    SessionResponse<RaceResultResponse>? Race);
