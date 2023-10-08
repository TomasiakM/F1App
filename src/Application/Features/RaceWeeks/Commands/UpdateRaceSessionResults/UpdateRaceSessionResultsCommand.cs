using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateRaceSessionResults;
public record UpdateRaceSessionResultsCommand(
    Guid RaceWeekId,
    List<UpdateRaceSessionResultCommand> SessionResults) : IRequest<Unit>;

public record UpdateRaceSessionResultCommand(
    int Place,
    int Laps,
    string? FastestLap,
    string FinishType,
    int StartPosition,
    string? FinishTime,
    float Points,
    string DriverId,
    string TeamId);