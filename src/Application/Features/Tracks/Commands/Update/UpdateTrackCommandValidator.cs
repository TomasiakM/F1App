using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Tracks.Commands.Update;
public sealed class UpdateTrackCommandValidator : AbstractValidator<UpdateTrackCommand>
{
    public UpdateTrackCommandValidator()
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

        RuleFor(e => e.Length)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty);

        RuleFor(e => e.Corners)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty);

        RuleFor(e => e.City)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength);
    }
}
