using Infrastructure.Interfaces;

namespace Infrastructure.Services;
internal sealed class HashService : IHashService
{
    public string Hash(string value)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        return BCrypt.Net.BCrypt.HashPassword(value, salt);
    }

    public bool Validate(string value, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(value, hash);
    }
}
