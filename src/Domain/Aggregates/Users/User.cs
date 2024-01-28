using Domain.Aggregates.Roles.ValueObjects;
using Domain.Aggregates.Users.Exceptions;
using Domain.Aggregates.Users.ValueObjects;
using Domain.DDD;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Aggregates.Users;
public sealed class User : AggregateRoot<UserId>
{
    private const string _userDefaultImage = "";

    private readonly List<Ban> _bans = new();
    private readonly List<RoleId> _roleIds = new();

    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public string Image { get; private set; } = _userDefaultImage;

    public IReadOnlyList<Ban> Bans => _bans.AsReadOnly();
    public IReadOnlyList<RoleId> RoleIds => _roleIds.AsReadOnly();

    private User(string username, string password, string email)
        : base(UserId.Create())
    {
        Username = username;
        Password = password;
        Email = email;

        _roleIds.Add(RoleId.UserRoleId);
    }

    public static User Create(string username, string password, string email)
    {
        return new User(username, password, email);
    }

    public void AddRole(RoleId roleId)
    {
        if (_roleIds.Any(e => e == roleId))
        {
            throw new DuplicateRoleException();
        }

        _roleIds.Add(roleId);
    }

    public void RemoveRole(RoleId roleId)
    {
        var role = _roleIds.FirstOrDefault(e => e == roleId);

        if (role is null)
        {
            throw new NotFoundException();
        }

        if (role == RoleId.UserRoleId)
        {
            throw new RoleCannotBeRemovedException();
        }

        _roleIds.Remove(roleId);
    }

    public void AddBan(DateTimeOffset end, string reason, IDateProvider dateProvider)
    {
        var userIsAdmin = RoleIds.Any(e => e == RoleId.AdminRoleId);

        if (userIsAdmin)
        {
            throw new CannotBanAdminAccountException();
        }

        if (GetActiveBan(dateProvider) is not null)
        {
            throw new OneOfBansIsStillActiveException();
        }

        var ban = Ban.Create(end, reason);
        _bans.Add(ban);
    }

    public Ban? GetActiveBan(IDateProvider dateProvider)
    {
        return _bans.FirstOrDefault(e => e.IsActive(dateProvider));
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }

#pragma warning disable CS8618
    private User() : base(UserId.Create()) { }
#pragma warning restore CS8618
}
