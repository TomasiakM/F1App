using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.Enums;
using Domain.Aggregates.Teams.ValueObjects;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateSprintSessionResults;
public record UpdateSprintSessionResultsCommand(
    Guid RaceWeekId,
    List<UpdateSprintSessionResultCommand> SessionResults) : IRequest<Unit>;

public record UpdateSprintSessionResultCommand(
    int Place, 
    int Laps, 
    string? FastestLap, 
    string FinishType, 
    int StartPosition, 
    string? FinishTime, 
    float Points, 
    string DriverId,
    string TeamId);
