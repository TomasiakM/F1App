using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateFP3SessionResults;
public record UpdateFP3SessionResultsCommand(
    Guid RaceWeekId,
    List<UpdateFP3SessionResultCommand> SessionResults) : IRequest<Unit>;


public record UpdateFP3SessionResultCommand(
    int Place,
    int Laps,
    string FastestLap,
    string DriverId,
    string TeamId);