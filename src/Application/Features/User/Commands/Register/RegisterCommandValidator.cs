using Application.Extensions;
using Application.Interfaces;
using Application.Validation.Messages;
using FluentValidation;

namespace Application.Features.User.Commands.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public RegisterCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(e => e.Username)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .MinimumLength(7).WithMessage(DefaultMessages.MinLength)
            .MaximumLength(21).WithMessage(DefaultMessages.MaxLength)
            .AlphaNumeric().WithMessage(DefaultMessages.AlphaNumeric)
            .MustAsync(UsernameAvailable).WithMessage(UserMessages.UsernameTaken);

        RuleFor(e => e.Password)
            .Password();

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage(DefaultMessages.NotEmpty)
            .EmailAddress().WithMessage(DefaultMessages.Email)
            .MustAsync(EmailAvailable).WithMessage(UserMessages.EmailTaken);
    }

    private async Task<bool> UsernameAvailable(string arg1, CancellationToken token)
    {
        var user = await _unitOfWork.Users.FindAsync(e => e.Username == arg1);

        return user is null;
    }

    private async Task<bool> EmailAvailable(string arg1, CancellationToken token)
    {
        var user = await _unitOfWork.Users.FindAsync(e => e.Email == arg1);

        return user is null;
    }
}
