namespace Application.Dtos.RaceWeek.Requests;
public record UpdateFreePracticeSessionResultsRequest(
    List<UpdateFreePracticeSessionResultRequest> SessionResults);

public record UpdateFreePracticeSessionResultRequest(
    int Place,
    int Laps,
    string FastestLap,
    string DriverId,
    string TeamId);