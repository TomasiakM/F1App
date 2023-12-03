using Domain.Exceptions;

namespace Domain.Aggregates.Ratings.Exceptions;
public sealed class RatingIsCreatedException : DomainException
{
    public RatingIsCreatedException() 
        : base("Aktualnie jest już prowadzona możliwość oceniania")
    {
    }
}
