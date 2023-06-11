using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Comments.Commands.Create;
public sealed class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(e => e.Text)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(2048).WithMessage(DefaultMessages.MaxLength);
    }
}
