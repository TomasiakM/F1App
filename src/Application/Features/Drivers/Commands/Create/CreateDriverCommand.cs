using MediatR;

namespace Application.Features.Drivers.Commands.Create;
public record CreateDriverCommand(
    string FirstName,
    string LastName,
    string Image,
    string DateOfBirth,
    string CountryCode,
    string DescriptionHtml) : IRequest<Unit>;
