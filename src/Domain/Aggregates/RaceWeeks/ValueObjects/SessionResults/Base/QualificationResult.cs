﻿using Domain.Aggregates.Drivers.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults.Base;
public class QualificationResult : ValueObject
{
    public int Place { get; private set; }
    public TimeSpan Q1Time { get; private set; }
    public TimeSpan? Q2Time { get; private set; }
    public TimeSpan? Q3Time { get; private set; }
    public DriverId DriverId { get; private set; }

    protected QualificationResult(int place, TimeSpan q1Time, TimeSpan q2Time, TimeSpan q3Time, DriverId driverId)
    {
        Place = place;
        Q1Time = q1Time;
        Q2Time = q2Time;
        Q3Time = q3Time;
        DriverId = driverId;
    }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Place;
        yield return Q1Time;
        yield return Q2Time;
        yield return Q3Time;
        yield return DriverId;
    }
}
