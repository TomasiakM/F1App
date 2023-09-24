using Application.Extensions;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Drivers.Commands.Create;
public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
{
    public CreateDriverCommandValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(255).WithMessage(DefaultMessages.MaxLength);

        RuleFor(e => e.LastName)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(255).WithMessage(DefaultMessages.MaxLength);

        RuleFor(e => e.Image)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty);

        RuleFor(e => e.DateOfBirth)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeDateTime().WithMessage(DefaultMessages.DateTimeOffset);

        RuleFor(e => e.CountryCode)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .Length(3).WithMessage(DefaultMessages.ExactLength);

        RuleFor(e => e.DescriptionHtml)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MaximumLength(16_384).WithMessage(DefaultMessages.MaxLength);
    }
}
