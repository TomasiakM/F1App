using Application.Interfaces;
using Domain.Aggregates.Users.ValueObjects;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.User.Commands.BanUser;
internal sealed class BanUserCommandHandler : IRequestHandler<BanUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;

    public BanUserCommandHandler(IUnitOfWork unitOfWork, IDateProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateTimeProvider;
    }

    public async Task<Unit> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(request.UserId);

        var user = await _unitOfWork.Users.GetAsync(userId);
        if(user is null)
        {
            throw new NotFoundException();
        }

        user.AddBan(
            _dateProvider.UtcNow.AddDays(request.BanDays), 
            request.Reason,
            _dateProvider);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
