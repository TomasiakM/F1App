namespace Application.Dtos.RaceWeek.Requests;
public record UpdateRaceSessionResultsRequest(
    List<UpdateRaceSessionResultRequest> SessionResults);

public record UpdateRaceSessionResultRequest(
    int Place,
    int Laps,
    string FastestLap,
    string FinishType,
    int StartPosition,
    string FinishTime,
    float Points,
    string DriverId,
    string TeamId);
