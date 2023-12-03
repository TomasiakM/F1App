using Domain.Exceptions;

namespace Domain.Aggregates.Ratings.Exceptions;
public class RatingIsNotFinishedException : DomainException
{
    public RatingIsNotFinishedException() 
        : base("Data zakończenia nie umożliwia podsumowania ocen")
    {
    }
}
