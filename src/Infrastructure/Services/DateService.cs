using Domain.Interfaces;

namespace Infrastructure.Services;
internal sealed class DateService : IDateProvider
{
    public DateTimeOffset Now => DateTime.Now;
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
