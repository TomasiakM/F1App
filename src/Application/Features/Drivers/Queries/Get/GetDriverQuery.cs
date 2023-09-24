using Application.Dtos.Driver.Responses;
using MediatR;

namespace Application.Features.Drivers.Queries.Get;
public record GetDriverQuery(
    string Slug) : IRequest<DriverResponse>;
