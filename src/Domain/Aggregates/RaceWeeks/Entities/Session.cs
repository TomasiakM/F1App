using Domain.DDD;

namespace Domain.Aggregates.RaceWeeks.Entities;
public sealed class Session<TSessionResult> : Entity<int>
{
    public DateTimeOffset Start { get; private set; }

    private List<TSessionResult> _sessionResults = new();
    public IReadOnlyList<TSessionResult> SessionResults => _sessionResults.AsReadOnly();

    private Session(DateTimeOffset start)
        : base(0)
    {
        Start = start;
    }

    public static Session<TSessionResult> Create(DateTimeOffset start)
        => new(start);

    private Session() : base(0) { }

    public void SetSessionStart(DateTimeOffset start)
    {
        Start = start;
    }

    public void SetSessionResults(List<TSessionResult> sessionResults)
    {
        _sessionResults = sessionResults;
    }
}
