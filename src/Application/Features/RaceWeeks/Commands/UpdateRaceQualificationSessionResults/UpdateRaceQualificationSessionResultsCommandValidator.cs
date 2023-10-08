using Application.Extensions;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.RaceWeeks.Commands.UpdateRaceQualificationSessionResults;
public sealed class UpdateRaceQualificationSessionResultsCommandValidator : AbstractValidator<UpdateRaceQualificationSessionResultsCommand>
{
    public UpdateRaceQualificationSessionResultsCommandValidator()
    {
        RuleFor(e => e.SessionResults)
            .Must(e => e.Select(sr => sr.DriverId).Count() == e.Select(sr => sr.DriverId).Distinct().Count()).WithMessage(RaceWeekMessages.UniqueDriverIdInCollection);

        RuleForEach(e => e.SessionResults)
            .Cascade(CascadeMode.Continue)
            .SetValidator(new UpdateRaceQualificationSessionResultCommandValidator());
    }
}

public sealed class UpdateRaceQualificationSessionResultCommandValidator : AbstractValidator<UpdateRaceQualificationSessionResultCommand>
{
    public UpdateRaceQualificationSessionResultCommandValidator()
    {
        RuleFor(e => e.Place)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .GreaterThanOrEqualTo(1).WithMessage(DefaultMessages.MinLength);

        RuleFor(e => e.Q1Time)
            .MustBeTimeSpan().WithMessage(DefaultMessages.MustBeTimeSpan);

        RuleFor(e => e.Q2Time)
            .MustBeTimeSpan().WithMessage(DefaultMessages.MustBeTimeSpan);

        RuleFor(e => e.Q3Time)
            .MustBeTimeSpan().WithMessage(DefaultMessages.MustBeTimeSpan);

        RuleFor(e => e.DriverId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeGuid().WithMessage(DefaultMessages.MustBeGuid);

        RuleFor(e => e.TeamId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeGuid().WithMessage(DefaultMessages.MustBeGuid);
    }
}
