using Domain.Aggregates.Roles;
using Domain.Aggregates.Users;
using Domain.Aggregates.Users.Exceptions;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;

namespace Test.Domain.Aggregates.Users;
public class UserTests
{
    private readonly Mock<IDateProvider> _dateProvider;

    public UserTests()
    {
        _dateProvider = new Mock<IDateProvider>();
    }

    [Fact]
    public void User_Create_ShouldCreateUser()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);

        Assert.Equal(username, user.Username);
        Assert.Equal(password, user.Password);
        Assert.Equal(email, user.Email);

        Assert.Single(user.RoleIds);
        Assert.Collection(
            user.RoleIds,
            e => Assert.Equal(Role.UserRole.Id, e));
    }

    [Fact]
    public void User_AddRole_ShouldAddAdminRole()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);
        user.AddRole(Role.AdminRole.Id);

        Assert.Equal(2, user.RoleIds.Count);
        Assert.Collection(
            user.RoleIds,
            e => Assert.Equal(Role.UserRole.Id, e),
            e => Assert.Equal(Role.AdminRole.Id, e));
    }

    [Fact]
    public void User_AddRole_ShouldThrowDuplicateRoleException()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);
        user.AddRole(Role.AdminRole.Id);

        Assert.Throws<DuplicateRoleException>(() => user.AddRole(Role.UserRole.Id));
        Assert.Throws<DuplicateRoleException>(() => user.AddRole(Role.AdminRole.Id));
    }

    [Fact]
    public void User_RemoveRole_ShouldRemoveAdminRole()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);
        user.AddRole(Role.AdminRole.Id);

        user.RemoveRole(Role.AdminRole.Id);

        Assert.Single(user.RoleIds);
        Assert.Collection(
            user.RoleIds,
            e => Assert.Equal(Role.UserRole.Id, e));
    }

    [Fact]
    public void User_RemoveRole_ShouldThrowNotFoundException()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);

        Assert.Throws<NotFoundException>(() => user.RemoveRole(Role.AdminRole.Id));
    }

    [Fact]
    public void User_RemoveRole_ShouldThrowRoleCannotBeRemovedException()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);

        Assert.Throws<RoleCannotBeRemovedException>(() => user.RemoveRole(Role.UserRole.Id));
    }

    [Fact]
    public void User_AddBan_ShouldAddBan()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);

        var end = new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero);
        var reason = "reason";
        var now = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _dateProvider
            .Setup(e => e.UtcNow)
            .Returns(now);

        user.AddBan(end, reason, _dateProvider.Object);

        Assert.Single(user.Bans);
        Assert.Equal(end, user.Bans.First().End);
        Assert.Equal(reason, user.Bans.First().Reason);
    }
    
    [Fact]
    public void User_GetActiveBan_ShouldReturnActiveBan()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);

        var end = new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero);
        var reason = "reason";
        var now = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _dateProvider
            .Setup(e => e.UtcNow)
            .Returns(now);

        user.AddBan(end, reason, _dateProvider.Object);

        var ban = user.GetActiveBan(_dateProvider.Object);
        Assert.NotNull(ban);
    }
    [Fact]
    public void User_GetActiveBan_ShouldReturnNull()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);

        var ban = user.GetActiveBan(_dateProvider.Object);

        Assert.Null(ban);
    }

    [Fact]
    public void User_GetActiveBan_ShouldReturnNull2()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);

        var end = new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero);
        var reason = "reason";

        var now = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);
        _dateProvider
            .Setup(e => e.UtcNow)
            .Returns(now);

        user.AddBan(end, reason, _dateProvider.Object);

        var now2 = new DateTimeOffset(2000, 1, 1, 0, 0, 31, TimeSpan.Zero);
        _dateProvider
            .Setup(e => e.UtcNow)
            .Returns(now2);

        var ban = user.GetActiveBan(_dateProvider.Object);
        Assert.Null(ban);
    }

    [Fact]
    public void User_UpdatePassword_ShouldUpdatePassword()
    {
        var username = "username";
        var password = "password";
        var email = "email";

        var user = User.Create(username, password, email);

        var updatedPassword = "updatedPassword";
        user.UpdatePassword(updatedPassword);

        Assert.Equal(updatedPassword, user.Password);
    }
}
