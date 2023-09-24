namespace Application.Dtos.RaceWeek.Requests;
public record CreateRaceWeekRequest(
    string Name,
    string SeasonId,
    string TrackId,
    string? FP1,
    string? FP2,
    string? FP3,
    string? SprintQualification,
    string? Sprint,
    string? RaceQualification,
    string? Race);
