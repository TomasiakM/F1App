using Application.Dtos.Common;
using Application.Dtos.Driver.Requests;
using Application.Features.Drivers.Commands.Create;
using Application.Features.Drivers.Commands.Delete;
using Application.Features.Drivers.Commands.Update;
using Application.Features.Drivers.Queries.Get;
using Application.Features.Drivers.Queries.GetAll;
using Application.Features.Drivers.Queries.GetPaginated;
using Application.Features.DriverStatistics.Queries.Get;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/driver")]
public sealed class DriverController : ControllerBase
{
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public DriverController(ISender mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [HttpGet("{driverSlug}")]
    public async Task<IActionResult> GetBySlug(string driverSlug)
    {
        var query = new GetDriverQuery(driverSlug);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("{driverId}/statistics")]
    public async Task<IActionResult> GetStatisctics(Guid driverId)
    {
        var query = new GetDriverStatisticQuery(driverId);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] PaginationFilters filters)
    {
        var query = new GetPaginatedDriversQuery(filters);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllDriversQuery();
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Create(CreateDriverRequest request)
    {
        var command = _mapper.Map<CreateDriverCommand>(request);
        await _mediatr.Send(command);

        return Ok();
    }

    [HttpPut("{driverId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Update(Guid driverId, UpdateDriverRequest request)
    {
        var command = _mapper.Map<UpdateDriverCommand>((driverId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [HttpDelete("{driverId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Delete(Guid driverId)
    {
        var command = new DeleteDriverCommand(driverId);
        await _mediatr.Send(command);

        return NoContent();
    }
}
