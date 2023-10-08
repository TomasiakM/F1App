﻿using Application.Extensions;
using Application.Validation.Messages;
using Domain.Aggregates.RaceWeeks.Enums;
using FluentValidation;

namespace Application.Features.RaceWeeks.Commands.UpdateRaceSessionResults;
public sealed class UpdateRaceSessionResultsCommandValidator : AbstractValidator<UpdateRaceSessionResultsCommand>
{
    public UpdateRaceSessionResultsCommandValidator()
    {
        RuleFor(e => e.SessionResults)
            .Must(e => e.Select(sr => sr.DriverId).Count() == e.Select(sr => sr.DriverId).Distinct().Count()).WithMessage(RaceWeekMessages.UniqueDriverIdInCollection);

        RuleForEach(e => e.SessionResults)
            .Cascade(CascadeMode.Continue)
            .SetValidator(new UpdateRaceSessionResultCommandValidator());
    }
}

public sealed class UpdateRaceSessionResultCommandValidator : AbstractValidator<UpdateRaceSessionResultCommand>
{
    public UpdateRaceSessionResultCommandValidator()
    {
        RuleFor(e => e.Place)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .GreaterThanOrEqualTo(1).WithMessage(DefaultMessages.MinLength);

        RuleFor(e => e.Laps)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .GreaterThanOrEqualTo(0).WithMessage(DefaultMessages.MinLength);

        RuleFor(e => e.StartPosition)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .GreaterThanOrEqualTo(1).WithMessage(DefaultMessages.MinLength);

        RuleFor(e => e.FinishTime)
            .MustBeTimeSpan().WithMessage(DefaultMessages.MustBeTimeSpan);

        RuleFor(e => e.FastestLap)
            .MustBeTimeSpan().WithMessage(DefaultMessages.MustBeTimeSpan);

        RuleFor(e => e.FinishType)
            .IsEnumName(typeof(FinishType));

        RuleFor(e => e.Points)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .GreaterThanOrEqualTo(0);

        RuleFor(e => e.DriverId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeGuid().WithMessage(DefaultMessages.MustBeGuid);

        RuleFor(e => e.TeamId)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeGuid().WithMessage(DefaultMessages.MustBeGuid);
    }
}
