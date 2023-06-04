using Application.Extensions;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.User.Commands.UpdatePassword;
public sealed class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(e => e.Password)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength);

        RuleFor(e => e.NewPassword)
            .Password()
            .NotEqual(req => req.Password).WithMessage(UserMessages.PasswordMustBeDifferent);
    }
}
