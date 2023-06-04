namespace Application.Validation.Messages;
internal static class DefaultMessages
{
    public static string NotEmpty = "Pole jest wymagane";
    public static string MinLength = "Pole musi mieć przynajmniej {MinLength} znaków";
    public static string MaxLength = "Pole może mieć maksymalnie {MaxLength} znaków";
    public static string Email = "Podaj poprawny email";
    public static string AlphaNumeric = "Pole może zawierać jedynie litery oraz cyfry";
    public static string PasswordRegex = "Hasło musi składać się z 1 małej oraz dużej litery, liczby i znaku specjalnego: ?!#@$%^&*(){}<>/\\-_=+";
}
