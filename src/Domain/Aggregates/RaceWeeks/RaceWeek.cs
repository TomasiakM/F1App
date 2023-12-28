using Domain.Aggregates.RaceWeeks.Entities;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Tracks.ValueObjects;
using Domain.DDD;
using Domain.Extensions;
using Domain.Interfaces;

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
        FP1 = Session<FP1Result>.Create(start);
    }

    public void UpdateFp1Session(DateTimeOffset start)
    {
        if (FP1 is null)
        {
            FP1 = Session<FP1Result>.Create(start);
            return;
        }

        FP1.SetSessionStart(start);
    }

    public void CreateFp2Session(DateTimeOffset start)
    {
        FP2 = Session<FP2Result>.Create(start);
    }

    public void UpdateFp2Session(DateTimeOffset start)
    {
        if (FP2 is null)
        {
            FP2 = Session<FP2Result>.Create(start);
            return;
        }

        FP2.SetSessionStart(start);
    }

    public void CreateFp3Session(DateTimeOffset start)
    {
        FP3 = Session<FP3Result>.Create(start);
    }

    public void UpdateFp3Session(DateTimeOffset start)
    {
        if (FP3 is null)
        {
            FP3 = Session<FP3Result>.Create(start);
            return;
        }

        FP3.SetSessionStart(start);
    }

    public void CreateSprintQualificationSession(DateTimeOffset start)
    {
        SprintQualifications = Session<SprintQualificationResult>.Create(start);
    }

    public void UpdateSprintQualificationSession(DateTimeOffset start)
    {
        if (SprintQualifications is null)
        {
            SprintQualifications = Session<SprintQualificationResult>.Create(start);
            return;
        }

        SprintQualifications.SetSessionStart(start);
    }

    public void CreateSprintSession(DateTimeOffset start)
    {
        Sprint = Session<SprintResult>.Create(start);
    }

    public void UpdateSprintSession(DateTimeOffset start)
    {
        if (Sprint is null)
        {
            Sprint = Session<SprintResult>.Create(start);
            return;
        }

        Sprint.SetSessionStart(start);
    }

    public void CreateRaceQualificationSession(DateTimeOffset start)
    {
        RaceQualifications = Session<RaceQualificationResult>.Create(start);
    }

    public void UpdateRaceQualificationSession(DateTimeOffset start)
    {
        if (RaceQualifications is null)
        {
            RaceQualifications = Session<RaceQualificationResult>.Create(start);
            return;
        }

        RaceQualifications.SetSessionStart(start);
    }

    public void CreateRaceSession(DateTimeOffset start)
    {
        Race = Session<RaceResult>.Create(start);
    }

    public void UpdateRaceSession(DateTimeOffset start)
    {
        if (Race is null)
        {
            Race = Session<RaceResult>.Create(start);
            return;
        }

        Race.SetSessionStart(start);
    }

    public List<SprintResult> GetAllSprintResults()
    {

        var results = new List<SprintResult>();

        if (Sprint is not null)
        {
            results.AddRange(Sprint.SessionResults);
        }

        return results;
    }

    public List<RaceResult> GetAllRaceResults()
    {

        var results = new List<RaceResult>();

        if (Race is not null)
        {
            results.AddRange(Race.SessionResults);
        }

        return results;
    }

    public bool IsReadyToStartRating(IDateProvider dateProvider)
    {
        if (Race is null ||
            Race.SessionResults.Count == 0 ||
            Race.Start > dateProvider.UtcNow &&
            dateProvider.UtcNow < Race.Start.AddDays(2))
        {
            return false;
        }


        return true;
    }

#pragma warning disable CS8618
    private RaceWeek() : base(RaceWeekId.Create()) { }
#pragma warning restore CS8618
}
