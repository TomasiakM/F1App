using Domain.Exceptions;

namespace Domain.Aggregates.Roles.Exceptions;
public sealed class InvalidRoleId : DomainException
{
    public InvalidRoleId() 
        : base("RoleId nie może zostać stworzona z tą wartością")
    {
    }
}
