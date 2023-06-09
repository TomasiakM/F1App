using Application.Extensions;
using Application.Interfaces;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.Tags.Commands.Create;
public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateTagCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(e => e.Name)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(3).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(15).WithMessage(DefaultMessages.MaxLength)
            .AlphaNumericWithSpaces().WithMessage(DefaultMessages.AlphaNumeric)
            .MustAsync(BeUnique).WithMessage(TagMessages.UniqueName);
    }

    private async Task<bool> BeUnique(string arg1, CancellationToken token)
    {
        var tag = await _unitOfWork.Tags.FindAsync(e => e.Name == arg1);

        return tag is null;
    }
}
