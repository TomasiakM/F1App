using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateSprintSessionResults;
public record SprintSessionResultsUpdatedEvent(
    Guid SeasonId) : INotification;
