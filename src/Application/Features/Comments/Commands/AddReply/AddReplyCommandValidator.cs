using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Comments.Commands.AddReply;
public sealed class AddReplyCommandValidator : AbstractValidator<AddReplyCommand>
{
    public AddReplyCommandValidator()
    {
        RuleFor(e => e.Text)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(2048).WithMessage(DefaultMessages.MaxLength);
    }
}
