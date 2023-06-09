using Domain.Exceptions;

namespace Domain.Aggregates.Articles.Exceptions;
public class PublishedDateCannotBeChangedException : DomainException
{
    public PublishedDateCannotBeChangedException() 
        : base("Nie można zmienić daty publikacji artykułu po jego opublikowaniu")
    {
    }
}
