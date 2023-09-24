using MediatR;

namespace Application.Features.Drivers.Commands.Delete;
public record DeleteDriverCommand(
    Guid DriverId) : IRequest<Unit>;
