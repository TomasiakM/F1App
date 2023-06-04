namespace Infrastructure.Settings;
internal sealed class JwtSettings
{
    public string Secret { get; set; } = "";
    public string Issuer { get; set; } = "";
    public int ExpiryMinutes { get; set; } = 10;
}
