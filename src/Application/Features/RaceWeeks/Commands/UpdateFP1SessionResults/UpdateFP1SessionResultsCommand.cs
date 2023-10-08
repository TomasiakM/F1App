using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateFP1SessionResults;
public record UpdateFP1SessionResultsCommand(
    Guid RaceWeekId,
    List<UpdateFP1SessionResultCommand> SessionResults) : IRequest<Unit>;


public record UpdateFP1SessionResultCommand(
    int Place,
    int Laps,
    string FastestLap,
    string DriverId,
    string TeamId);