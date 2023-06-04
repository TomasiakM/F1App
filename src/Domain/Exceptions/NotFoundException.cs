namespace Domain.Exceptions;
public sealed class NotFoundException : Exception
{
    public NotFoundException() 
        : base("Nie odnaleziono zasobu")
    {
    }
}
