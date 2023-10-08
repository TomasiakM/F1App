using Application.Extensions;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.RaceWeeks.Commands.Update;
public sealed class UpdateRaceWeekCommandValidator : AbstractValidator<UpdateRaceWeekCommand>
{
    public UpdateRaceWeekCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(255).WithMessage(DefaultMessages.MaxLength);

        RuleFor(e => e.TrackId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeGuid().WithMessage(DefaultMessages.MustBeGuid);

        RuleFor(e => e.FP1).MustBeDateTimeOffset().WithMessage(DefaultMessages.DateTimeOffset);
        RuleFor(e => e.FP2).MustBeDateTimeOffset().WithMessage(DefaultMessages.DateTimeOffset);
        RuleFor(e => e.FP3).MustBeDateTimeOffset().WithMessage(DefaultMessages.DateTimeOffset);

        RuleFor(e => e.SprintQualification).MustBeDateTimeOffset().WithMessage(DefaultMessages.DateTimeOffset);
        RuleFor(e => e.Sprint).MustBeDateTimeOffset().WithMessage(DefaultMessages.DateTimeOffset); ;

        RuleFor(e => e.RaceQualification).MustBeDateTimeOffset().WithMessage(DefaultMessages.DateTimeOffset);
        RuleFor(e => e.Race).MustBeDateTimeOffset().WithMessage(DefaultMessages.DateTimeOffset);
    }
}
