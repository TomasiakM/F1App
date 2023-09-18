using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Seasons;
public interface ISeasonRepository : IRepository<Season, SeasonId>
{
}
