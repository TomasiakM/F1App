using Domain.Aggregates.RaceWeeks.Entities;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Tracks.ValueObjects;
using Domain.DDD;
using Domain.Extensions;

namespace Domain.Aggregates.RaceWeeks;
public sealed class RaceWeek : AggregateRoot<RaceWeekId>
{
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public TrackId TrackId { get; private set; }
    public SeasonId SeasonId { get; private set; }

    public Session<FP1Result>? FP1 { get; private set; } = null;
    public Session<FP2Result>? FP2 { get; private set; } = null;
    public Session<FP3Result>? FP3 { get; private set; } = null;
    public Session<SprintQualificationResult>? SprintQualifications { get; private set; } = null;
    public Session<RaceQualificationResult>? RaceQualifications { get; private set; } = null;
    public Session<SprintResult>? Sprint { get; private set; } = null;
    public Session<RaceResult>? Race { get; private set; } = null;


    private RaceWeek(string name, TrackId trackId, SeasonId seasonId) 
        : base(RaceWeekId.Create())
    {
        Name = name;
        Slug = name.ToUrlFriendly();

        TrackId = trackId;
        SeasonId = seasonId;
    }

    public static RaceWeek Create(string name, TrackId trackId, SeasonId seasonId) 
        => new(name, trackId, seasonId);

    public void Update(string name, TrackId trackId)
    {
        Name = name;
        Slug = name.ToUrlFriendly();

        TrackId = trackId;
    }

    public void CreateFp1Session(DateTimeOffset start)
    {
        FP1 = new(start);    
    }

    public void CreateFp2Session(DateTimeOffset start)
    {
        FP2 = new(start);
    }

    public void CreateFp3Session(DateTimeOffset start)
    {
        FP3 = new(start);
    }

    public void CreateSprintQualificationSession(DateTimeOffset start)
    {
        SprintQualifications = new(start);
    }

    public void CreateSprintSession(DateTimeOffset start)
    {
        Sprint = new(start);
    }

    public void CreateRaceQualificationSession(DateTimeOffset start)
    {
        RaceQualifications = new(start);
    }

    public void CreateRaceSession(DateTimeOffset start)
    {
        Race = new(start);
    }

#pragma warning disable CS8618
    private RaceWeek() : base(RaceWeekId.Create()) { }
    #pragma warning restore CS8618
}
