using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateRaceSessionResults;
public record RaceSessionResultsUpdatedEvent(
    Guid RaceWeekId, 
    Guid SeasonId) : INotification;
