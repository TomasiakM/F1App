﻿using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
public sealed class RaceQualificationResult : QualificationResult
{
    private RaceQualificationResult(int place, TimeSpan q1Time, TimeSpan q2Time, TimeSpan q3Time, DriverId driverId)
        : base(place, q1Time, q2Time, q3Time, driverId)
    {
    }

    public static RaceQualificationResult Create(int place, TimeSpan q1Time, TimeSpan q2Time, TimeSpan q3Time, DriverId driverId)
        => new(place, q1Time, q2Time, q3Time, driverId);

    private RaceQualificationResult() : base(0, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, DriverId.Create()) { }
}
