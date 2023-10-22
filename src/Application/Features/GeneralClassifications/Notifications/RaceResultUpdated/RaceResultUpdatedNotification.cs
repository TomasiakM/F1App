using MediatR;

namespace Application.Features.GeneralClassifications.Notifications.RaceResultUpdated;
public record RaceResultUpdatedNotification(
    Guid SeasonId) : INotification;
