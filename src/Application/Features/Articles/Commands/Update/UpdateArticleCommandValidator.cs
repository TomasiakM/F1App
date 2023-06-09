using Application.Extensions;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Articles.Commands.Update;
public sealed class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleCommandValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(64).WithMessage(DefaultMessages.MaxLength);

        RuleFor(e => e.Image)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty);

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(255).WithMessage(DefaultMessages.MaxLength);

        RuleFor(e => e.DescriptionHtml)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MaximumLength(16_384).WithMessage(DefaultMessages.MaxLength);

        RuleFor(e => e.PublishedAt)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MustBeDateTimeOffset().WithMessage(DefaultMessages.DateTimeOffset);

        RuleFor(e => e.TagIds)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .Must(e => e.All(e => e.MustBeGuid())).WithMessage(DefaultMessages.AllMustBeGuid);
    }
}
