using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateSprintQualificationSessionResults;
public record UpdateSprintQualificationSessionResultsCommand(
    Guid RaceWeekId,
    List<UpdateSprintQualificationSessionResultCommand> SessionResults) : IRequest<Unit>;

public record UpdateSprintQualificationSessionResultCommand(
    int Place, 
    string? Q1Time, 
    string? Q2Time, 
    string? Q3Time,
    string DriverId,
    string TeamId);