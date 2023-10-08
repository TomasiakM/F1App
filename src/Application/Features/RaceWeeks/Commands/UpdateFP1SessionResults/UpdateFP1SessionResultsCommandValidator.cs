using Application.Extensions;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.RaceWeeks.Commands.UpdateFP1SessionResults;
public sealed class UpdateFP1SessionResultsCommandValidator : AbstractValidator<UpdateFP1SessionResultsCommand>
{
    public UpdateFP1SessionResultsCommandValidator()
    {
        RuleFor(e => e.SessionResults)
            .Must(e => e.Select(sr => sr.DriverId).Count() == e.Select(sr => sr.DriverId).Distinct().Count()).WithMessage(RaceWeekMessages.UniqueDriverIdInCollection);

        RuleForEach(e => e.SessionResults)
            .Cascade(CascadeMode.Continue)
            .SetValidator(new UpdateFP1SessionResultCommandValidator());
    }
}

public sealed class UpdateFP1SessionResultCommandValidator : AbstractValidator<UpdateFP1SessionResultCommand>
{
    public UpdateFP1SessionResultCommandValidator()
    {
        RuleFor(e => e.Place)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .GreaterThanOrEqualTo(1).WithMessage(DefaultMessages.MinLength);

        RuleFor(e => e.Laps)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .GreaterThanOrEqualTo(0).WithMessage(DefaultMessages.MinLength);

        RuleFor(e => e.FastestLap)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeTimeSpan().WithMessage(DefaultMessages.MustBeTimeSpan);

        RuleFor(e => e.DriverId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeGuid().WithMessage(DefaultMessages.MustBeGuid);

        RuleFor(e => e.TeamId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeGuid().WithMessage(DefaultMessages.MustBeGuid);
    }
}
