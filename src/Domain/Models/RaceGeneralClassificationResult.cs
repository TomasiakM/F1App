using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;

namespace Application.Dtos.GeneralClassification.Common;
public record RaceGeneralClassificationResult(
    DriverId DriverId,
    TeamId TeamId,
    float Points,
    int Position);
