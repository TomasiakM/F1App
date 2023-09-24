using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Drivers.Commands.Update;
internal sealed class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDriverCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var driverId = DriverId.Create(request.DriverId);
        var driver = await _unitOfWork.Drivers.GetAsync(driverId, cancellationToken);

        if(driver is null)
        {
            throw new NotFoundException();
        }

        driver.Update(
            request.FirstName, 
            request.LastName, 
            DateTime.Parse(request.DateOfBirth), 
            request.CountryCode, 
            request.Image,
            request.DescriptionHtml);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
