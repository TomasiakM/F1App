using MediatR;

namespace Application.Features.RaceWeeks.Commands.UpdateRaceQualificationSessionResults;
public record RaceQualificationSessionResultsEvent(
    Guid RaceWeekId, Guid SeasonId) : INotification;
