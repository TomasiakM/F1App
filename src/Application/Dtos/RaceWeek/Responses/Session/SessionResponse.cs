namespace Application.Dtos.RaceWeek.Responses.Session;
public record SessionResponse<TSessionResult>(
    DateTimeOffset Start,
    List<TSessionResult> SessionResults);
