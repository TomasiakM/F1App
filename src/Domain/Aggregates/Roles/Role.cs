using Domain.Aggregates.Roles.ValueObjects;
using Domain.DDD;

namespace Domain.Aggregates.Roles;
public sealed class Role : AggregateRoot<RoleId>
{
    public string Name { get; private set; }

    private Role(RoleId id, string name)
        :base(id)
    {
        Name = name;
    }

    public const string UserRoleName = "User";
    public const string AdminRoleName = "Admin";

    public static Role UserRole => new(RoleId.UserRoleId, UserRoleName);
    public static Role AdminRole => new(RoleId.AdminRoleId, AdminRoleName);

    public static List<Role> GetRolesByIds(List<RoleId> roleIds)
    {
        var list = new List<Role>();

        if(roleIds.Any(e => e == RoleId.UserRoleId))
        {
            list.Add(UserRole);
        }

        if(roleIds.Any(e => e == RoleId.AdminRoleId))
        {
            list.Add(AdminRole);
        }

        return list;
    }

    #pragma warning disable CS8618
    private Role() : base(RoleId.UserRoleId) { }
    #pragma warning restore CS8618
}
