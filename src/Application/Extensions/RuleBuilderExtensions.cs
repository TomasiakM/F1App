using Application.Validation.Messages;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Extensions;
public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> AlphaNumeric<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches("^[a-zA-Z0-9]*$");
    }

    public static IRuleBuilderOptions<T, string> AlphaNumericWithSpaces<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches("^[a-zA-Z0-9 ]*$");
    }

    public static IRuleBuilderOptions<T, string> PasswordRegex<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[?!#@$%^&*(){}<>/\\-_=+])[A-Za-z\\d?!#@$%^&*(){}<>/\\-_=+]{1,}$");
    }

    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(8).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(32).WithMessage(DefaultMessages.MaxLength)
            .PasswordRegex().WithMessage(DefaultMessages.PasswordRegex);
    }

    public static IRuleBuilderOptions<T, string> MustBeGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Matches("^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$");
    }

    public static bool MustBeGuid(this string str)
    {
        return Regex.IsMatch(str, "^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$");
    }

    public static IRuleBuilderOptions<T, string?> MustBeDateTimeOffset<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(e =>
            {
                if(e is not null)
                {
                    return DateTimeOffset.TryParse(e, out var dt);
                }

                return true;
            });
    }

    public static IRuleBuilderOptions<T, string> MustBeDateTime<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(e => DateTime.TryParse(e, out var dt));
    }
}
