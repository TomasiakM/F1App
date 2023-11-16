using Domain.Exceptions;

namespace Domain.Aggregates.Ratings.Exceptions;
public sealed class InvalidDriverRatingsException : DomainException
{
    public InvalidDriverRatingsException() 
        : base("Oceny muszą zawierać wszyskich kierowców uczestniczących w ocenie")
    {
    }
}
