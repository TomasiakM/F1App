using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Seasons.Commands.Create;
public class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
{
    public CreateSeasonCommandValidator()
    {
        RuleFor(e => e.Year)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .GreaterThanOrEqualTo(1900).WithMessage(DefaultMessages.MinLength)
            .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage(DefaultMessages.MaxLength);
    }
}
