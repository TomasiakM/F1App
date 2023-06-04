using Domain.Exceptions;

namespace Domain.Aggregates.Users.Exceptions;
public sealed class DuplicateRoleException : DomainException
{
    public DuplicateRoleException() 
        : base("Użytkownik nie może posiadać 2 takich samych ról")
    {
    }
}
