using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.RaceWeeks;
public interface IRaceWeekRepository : IRepository<RaceWeek, RaceWeekId>
{
}
