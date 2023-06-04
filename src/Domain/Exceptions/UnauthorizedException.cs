namespace Domain.Exceptions;
public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException() 
        : base("Brak autoryzacji")
    {
    }
}
