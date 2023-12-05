using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.DriverStatistics.ValueObjects;
using Domain.Aggregates.RaceWeeks;
using Domain.DDD;

namespace Domain.Aggregates.DriverStatistics;
public sealed class DriverStatistic : AggregateRoot<DriverStatisticId>
{
    public DriverId DriverId { get; private set; }
    public int Races { get; private set; }
    public int Poles { get; private set; }
    public int Wins { get; private set; }
    public int Podiums { get; private set; }

    private DriverStatistic(DriverId driverId)
        : base(DriverStatisticId.Create())
    {
        DriverId = driverId;
    }

    public static DriverStatistic Create(DriverId driverId) =>
        new(driverId);

    public void UpdateStatistics(ICollection<RaceWeek> raceWeeks)
    {
        var racesWithDriver = raceWeeks
            .Where(e => e.Race is not null && e.Race.SessionResults.Any(sr => sr.DriverId == DriverId))
            .Select(e => e.Race);

        var qualificationsWithDriver = raceWeeks
            .Where(e => e.RaceQualifications is not null && e.RaceQualifications.SessionResults.Any(sr => sr.DriverId == DriverId))
            .Select(e => e.RaceQualifications);

        Races = racesWithDriver.Count();
        Podiums = racesWithDriver.Where(e => e!.SessionResults.Any(sr => sr.Place <= 3)).Count();
        Wins = racesWithDriver.Where(e => e!.SessionResults.Any(sr => sr.Place == 1)).Count();

        Poles = qualificationsWithDriver.Where(e => e!.SessionResults.Any(sr => sr.Place == 1)).Count();
    }

#pragma warning disable CS8618
    private DriverStatistic() : base(DriverStatisticId.Create()) { }
#pragma warning restore CS8618
}
