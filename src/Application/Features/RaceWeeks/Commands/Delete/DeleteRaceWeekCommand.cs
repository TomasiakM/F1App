using MediatR;

namespace Application.Features.RaceWeeks.Commands.Delete;
public record DeleteRaceWeekCommand(
    Guid RaceWeekId) : IRequest<Unit>;
