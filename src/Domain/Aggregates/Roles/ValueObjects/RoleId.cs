using Domain.DDD;

namespace Domain.Aggregates.Roles.ValueObjects;
public sealed class RoleId : ValueObject
{
    public Guid Value { get; init; }

    private RoleId()
    {
        Value = Guid.NewGuid();
    }

    private RoleId(Guid value)
    {
        Value = value;
    }

    private static RoleId Create()
    {
        return new RoleId(Guid.NewGuid());
    }

    public static RoleId Create(Guid value)
    {
        return new RoleId(value);
    }

    public static RoleId UserRoleId => Create(new Guid("208b035e-c096-43c1-a05e-98e458573016"));
    public static RoleId AdminRoleId => Create(new Guid("2c8a29a3-f4e6-4b4c-860f-bfde6c949b0d"));
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
