using MediatR;

namespace Application.Features.RaceWeeks.Commands.Create;
public record CreateRaceWeekCommand(
    string Name,
    string TrackId,
    string SeasonId,
    string? FP1,
    string? FP2,
    string? FP3,
    string? SprintQualification,
    string? Sprint,
    string? RaceQualification,
    string? Race) : IRequest<Unit>;
