using Application.Validation.Messages;
using FluentValidation;

namespace Application.Extensions;
public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> AlphaNumeric<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches("^[a-zA-Z0-9]*$");
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

    public static IRuleBuilder<T, string> Guid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Matches("^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$");
    }
}
