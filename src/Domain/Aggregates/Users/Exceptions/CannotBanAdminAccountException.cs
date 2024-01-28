using Domain.Exceptions;

namespace Domain.Aggregates.Users.Exceptions;
public class CannotBanAdminAccountException : DomainException
{
    public CannotBanAdminAccountException()
        : base("Nie można zbanować użytkownika z rolą administratora")
    {
    }
}
