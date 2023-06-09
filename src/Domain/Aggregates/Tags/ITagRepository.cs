using Domain.Aggregates.Tags.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.Tags;
public interface ITagRepository : IRepository<Tag, TagId>
{
}
