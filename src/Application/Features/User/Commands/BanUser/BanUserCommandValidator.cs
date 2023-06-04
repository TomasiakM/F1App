using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.User.Commands.BanUser;
public sealed class BanUserCommandValidator : AbstractValidator<BanUserCommand>
{
    public BanUserCommandValidator()
    {
        RuleFor(e => e.BanDays)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .InclusiveBetween(1, 31).WithMessage(UserMessages.BanPeriodBeetween);

        RuleFor(e => e.Reason)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(64).WithMessage(DefaultMessages.MaxLength);
    }
}
