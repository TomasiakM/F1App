using Application.Interfaces;
using Domain.Aggregates.Drivers;
using MediatR;

namespace Application.Features.Drivers.Commands.Create;
internal sealed class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDriverCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = Driver.Create(
            request.FirstName, 
            request.LastName, 
            DateTime.Parse(request.DateOfBirth), 
            request.CountryCode, 
            request.Image, 
            request.DescriptionHtml);

        _unitOfWork.Drivers.Add(driver);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
