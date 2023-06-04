namespace Domain.Exceptions;
public class ForbiddenException : Exception
{
    public ForbiddenException()
        : base("Brak dostępu")
    {
    }

    protected ForbiddenException(string message) 
        : base(message) 
    {
    }
}
