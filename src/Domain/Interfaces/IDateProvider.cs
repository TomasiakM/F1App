namespace Domain.Interfaces;
public interface IDateProvider
{
    DateTimeOffset Now { get; }
    DateTimeOffset UtcNow { get; }
}
