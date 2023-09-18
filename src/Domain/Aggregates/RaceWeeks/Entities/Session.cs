using Domain.DDD;

namespace Domain.Aggregates.RaceWeeks.Entities;
public sealed class Session<TSessionResult> : Entity<int>
{
    public DateTimeOffset Start { get; private set; }

    private List<TSessionResult> _sessionResults = new();
    public IReadOnlyList<TSessionResult> SessionResults => _sessionResults.AsReadOnly();

    public Session(DateTimeOffset start)
        : base(0)
    {
        Start = start;
    }

    private Session() : base(0) { }
}
