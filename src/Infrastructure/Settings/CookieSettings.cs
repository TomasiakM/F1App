namespace Infrastructure.Settings;
internal sealed class CookieSettings
{
    public string Name { get; set; } = "";
    public int ExpiryDays { get; set; } = 30;
}
