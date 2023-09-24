using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Drivers.Commands.Delete;
internal sealed class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDriverCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
    {
        var driverId = DriverId.Create(request.DriverId);
        var driver = await _unitOfWork.Drivers.GetAsync(driverId);

        if(driver is null)
        {
            throw new NotFoundException();
        }

        _unitOfWork.Drivers.Delete(driver);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
