using Domain.Aggregates.Roles.Exceptions;
using Domain.DDD;

namespace Domain.Aggregates.Roles.ValueObjects;
public sealed class RoleId : ValueObject
{
    public Guid Value { get; init; }

    private RoleId() => Value = Guid.NewGuid();
    private RoleId(Guid value) => Value = value;

    public static RoleId Create(Guid value)
    {
        var roleId = new RoleId(value);

        if(!(roleId == UserRoleId || roleId == AdminRoleId))
        {
            throw new InvalidRoleId();
        }

        return roleId;
    }

    public static RoleId UserRoleId => new(new Guid("208b035e-c096-43c1-a05e-98e458573016"));
    public static RoleId AdminRoleId => new(new Guid("2c8a29a3-f4e6-4b4c-860f-bfde6c949b0d"));
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
