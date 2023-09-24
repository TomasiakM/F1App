using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Teams.Commands.Create;
public sealed class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MaximumLength(255).WithMessage(DefaultMessages.MaxLength);

        RuleFor(e => e.Image)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty);

        RuleFor(e => e.CountryCode)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .Length(3).WithMessage(DefaultMessages.ExactLength);

        RuleFor(e => e.DescriptionHtml)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MaximumLength(16_384).WithMessage(DefaultMessages.MaxLength);
    }
}
