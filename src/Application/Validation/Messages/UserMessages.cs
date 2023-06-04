namespace Application.Validation.Messages;
internal static class UserMessages
{
    public static string EmailTaken = "Ten email jest już zajęty";
    public static string UsernameTaken = "Użytkownik z tą nazwą już istnieje";
    public static string PasswordMustBeDifferent = "Hasła muszą się różnić";
    public static string BanPeriodBeetween = "Wartość musi mieścić się w zakresie {From} - {To}";
}
