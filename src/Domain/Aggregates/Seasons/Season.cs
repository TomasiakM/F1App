using Domain.Aggregates.Seasons.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.Seasons;
public sealed class Season : AggregateRoot<SeasonId>
{
    public int Year { get; private set; }

    private Season(int year) 
        : base(SeasonId.Create())
    {
        Year = year;
    }

    public static Season Create(int year) => new(year);

    #pragma warning disable CS8618
    private Season() : base(SeasonId.Create()) { }
    #pragma warning restore CS8618
}
