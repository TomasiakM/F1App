using Domain.Aggregates.Tags;
using Domain.Aggregates.Tags.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class TagRepository : GenericRepository<Tag, TagId>, ITagRepository
{
    public TagRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
