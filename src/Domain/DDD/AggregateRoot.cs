using Domain.Interfaces;

namespace Domain.DDD;
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : notnull
{
    protected AggregateRoot(TId id)
        : base(id)
    {
    }
}
