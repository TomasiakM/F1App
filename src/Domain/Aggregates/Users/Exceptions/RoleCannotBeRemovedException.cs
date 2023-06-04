using Domain.Exceptions;

namespace Domain.Aggregates.Users.Exceptions;
public sealed class RoleCannotBeRemovedException : DomainException
{
    public RoleCannotBeRemovedException() 
        : base("Rola nie może zostać usunięta")
    {
    }
}
