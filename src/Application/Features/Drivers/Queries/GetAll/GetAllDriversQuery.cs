using Application.Dtos.Driver.Responses;
using MediatR;

namespace Application.Features.Drivers.Queries.GetAll;
public record GetAllDriversQuery() : IRequest<List<DriverResponse>>; 
