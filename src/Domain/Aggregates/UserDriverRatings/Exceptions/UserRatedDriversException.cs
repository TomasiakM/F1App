using Domain.Exceptions;

namespace Domain.Aggregates.UserDriverRatings.Exceptions;
public class UserRatedDriversException : DomainException
{
    public UserRatedDriversException() 
        : base("Już oddałeś swoje oceny")
    {
    }
}
