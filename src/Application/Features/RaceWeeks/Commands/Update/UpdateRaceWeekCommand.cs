using MediatR;

namespace Application.Features.RaceWeeks.Commands.Update;
public record UpdateRaceWeekCommand(
    Guid RaceWeekId,
    string Name,
    string TrackId,
    string? FP1,
    string? FP2,
    string? FP3,
    string? SprintQualification,
    string? Sprint,
    string? RaceQualification,
    string? Race) : IRequest<Unit>;
