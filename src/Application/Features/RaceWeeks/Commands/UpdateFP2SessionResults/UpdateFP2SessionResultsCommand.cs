using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateFP2SessionResults;
public record UpdateFP2SessionResultsCommand(
    Guid RaceWeekId,
    List<UpdateFP2SessionResultCommand> SessionResults) : IRequest<Unit>;


public record UpdateFP2SessionResultCommand(
    int Place, 
    int Laps, 
    string FastestLap,
    string DriverId,
    string TeamId);