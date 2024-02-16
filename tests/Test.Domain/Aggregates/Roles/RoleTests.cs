
using Domain.Aggregates.Roles;

namespace Test.Domain.Aggregates.Roles;
public class RoleTests
{
    [Fact]
    public void Role_GetRoleByIds_ShouldReturnListWithUserRole()
    {
        Assert.Single(Role.GetRolesByIds(new() { Role.UserRole.Id }));
    }

    [Fact]
    public void Role_GetRoleByIds_ShouldReturnListWithAdminRole()
    {
        Assert.Single(Role.GetRolesByIds(new() { Role.AdminRole.Id }));
    }

    [Fact]
    public void Role_GetRoleByIds_ShouldReturnListWithUserAndAdminRole()
    {
        Assert.Equal(2, Role.GetRolesByIds(new() { Role.UserRole.Id, Role.AdminRole.Id }).Count);
    }
}
