using Domain.Exceptions;

namespace Domain.Aggregates.Ratings.Exceptions;
public sealed class RatingForRaceWeekIsCreatedException : DomainException
{
    public RatingForRaceWeekIsCreatedException() 
        : base("Możliwość oceniania jest już utworzona dla tego wydarzenia")
    {
    }
}
