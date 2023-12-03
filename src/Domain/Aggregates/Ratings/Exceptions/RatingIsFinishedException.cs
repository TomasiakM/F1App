using Domain.Exceptions;

namespace Domain.Aggregates.Ratings.Exceptions;
public class RatingIsFinishedException : DomainException
{
    public RatingIsFinishedException() 
        : base("Zakończyła się możliwość dodawania ocen")
    {
    }
}
