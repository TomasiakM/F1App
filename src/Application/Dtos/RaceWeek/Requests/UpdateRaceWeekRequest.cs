namespace Application.Dtos.RaceWeek.Requests;
public record UpdateRaceWeekRequest(
    string Name,
    string TrackId,
    string? FP1,
    string? FP2,
    string? FP3,
    string? SprintQualification,
    string? Sprint,
    string? RaceQualification,
    string? Race);
