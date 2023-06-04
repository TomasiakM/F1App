using Domain.Exceptions;

namespace Domain.Aggregates.Users.Exceptions;
public sealed class OneOfBansIsStillActiveException : DomainException
{
    public OneOfBansIsStillActiveException() 
        : base("Użytkownik aktualnie posiada aktywnego bana")
    {
    }
}
