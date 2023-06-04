using MediatR;

namespace Application.Features.User.Commands;
public record BanUserCommand(
    Guid UserId,
    int BanDays,
    string Reason) : IRequest<Unit>;
