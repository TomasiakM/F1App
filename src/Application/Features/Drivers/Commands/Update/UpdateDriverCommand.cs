using MediatR;

namespace Application.Features.Drivers.Commands.Update;
public record UpdateDriverCommand(
    Guid DriverId,
    string FirstName,
    string LastName,
    string Image,
    string DateOfBirth,
    string CountryCode,
    string DescriptionHtml) : IRequest<Unit>;
