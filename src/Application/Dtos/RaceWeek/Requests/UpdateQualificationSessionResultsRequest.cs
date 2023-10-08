public record UpdateQualificationSessionResultsRequest(
    List<UpdateQualificationSessionResultRequest> SessionResults);

public record UpdateQualificationSessionResultRequest(
    int Place,
    string? Q1Time,
    string? Q2Time,
    string? Q3Time,
    string DriverId,
    string TeamId);