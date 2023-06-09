using Domain.Exceptions;

namespace Domain.Aggregates.Articles.Exceptions;
public sealed class PublishDateCannotBeSetToPastException : DomainException
{
    public PublishDateCannotBeSetToPastException() 
        : base("Data publikacji nie może być z przeszłości")
    {
    }
}
