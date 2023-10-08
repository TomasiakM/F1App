using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateRaceQualificationSessionResults;
public record UpdateRaceQualificationSessionResultsCommand(
    Guid RaceWeekId,
    List<UpdateRaceQualificationSessionResultCommand> SessionResults) : IRequest<Unit>;

public record UpdateRaceQualificationSessionResultCommand(
    int Place,
    string? Q1Time,
    string? Q2Time,
    string? Q3Time,
    string DriverId,
    string TeamId);