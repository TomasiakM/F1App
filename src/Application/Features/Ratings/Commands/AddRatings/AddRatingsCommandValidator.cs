using Application.Extensions;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Ratings.Commands.AddRatings;
public sealed class AddRatingsCommandValidator : AbstractValidator<AddRatingsCommand>
{
    public AddRatingsCommandValidator()
    {
        RuleFor(e => e.RatingId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty);

        RuleForEach(e => e.Ratings)
            .Cascade(CascadeMode.Continue)
            .SetValidator(new DriverRatingCommandValidator());
    }
}


public sealed class DriverRatingCommandValidator : AbstractValidator<DriverRatingCommand>
{
    public DriverRatingCommandValidator()
    {
        RuleFor(e => e.DriverId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeGuid().WithMessage(DefaultMessages.MustBeGuid);

        RuleFor(e => e.Rating)
            .NotNull().WithMessage(DefaultMessages.NotEmpty)
            .InclusiveBetween(1, 10).WithMessage("Wybierz wartość z zakresu {From} - {To}");
    }
}