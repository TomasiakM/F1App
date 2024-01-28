using Domain.Exceptions;

namespace Domain.Aggregates.Users.Exceptions;
public sealed class InvalidPasswordException : DomainException
{
    public InvalidPasswordException()
        : base("Wprowadzone hasło jest niepoprawne")
    {
    }
}
