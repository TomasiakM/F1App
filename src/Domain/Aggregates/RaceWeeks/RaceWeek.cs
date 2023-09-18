using Domain.Aggregates.RaceWeeks.Entities;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Tracks.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.RaceWeeks;
public sealed class RaceWeek : AggregateRoot<RaceWeekId>
{
    public int Round { get; private set; }
    public string Name { get; private set; }

    public TrackId TrackId { get; private set; }
    public SeasonId SeasonId { get; private set; }

    public Session<FP1Result>? FP1 { get; private set; } = null;
    public Session<FP2Result>? FP2 { get; private set; } = null;
    public Session<FP3Result>? FP3 { get; private set; } = null;
    public Session<SprintQualificationResult>? SprintQualifications { get; private set; } = null;
    public Session<RaceQualificationResult>? RaceQualifications { get; private set; } = null;
    public Session<SprintResult>? Sprint { get; private set; } = null;
    public Session<RaceResult>? Race { get; private set; } = null;


    private RaceWeek(int round, string name, TrackId trackId, SeasonId seasonId) 
        : base(RaceWeekId.Create())
    {
        Round = round;
        Name = name;
        TrackId = trackId;
        SeasonId = seasonId;
    }

    public static RaceWeek Create(int round, string name, TrackId trackId, SeasonId seasonId) 
        => new(round, name, trackId, seasonId);

    public void Update(int round, string name, TrackId trackId)
    {
        Round = round;
        Name = name;
        TrackId = trackId;
    }

    #pragma warning disable CS8618
    private RaceWeek() : base(RaceWeekId.Create()) { }
    #pragma warning restore CS8618
}
