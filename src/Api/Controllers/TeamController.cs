using Application.Dtos.Common;
using Application.Dtos.Team.Requests;
using Application.Features.Teams.Commands.Create;
using Application.Features.Teams.Commands.Delete;
using Application.Features.Teams.Commands.Update;
using Application.Features.Teams.Queries.Get;
using Application.Features.Teams.Queries.GetPaginated;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/team")]
public sealed class TeamController : ControllerBase
{
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public TeamController(ISender mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] PaginationFilters filters)
    {
        var query = new GetPaginatedTeamsQuery(filters);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("{teamSlug}")]
    public async Task<IActionResult> GetBySlug(string teamSlug)
    {
        var query = new GetTeamQuery(teamSlug);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Create(CreateTeamRequest request)
    {
        var command = _mapper.Map<CreateTeamCommand>(request);
        await _mediatr.Send(command);

        return Ok();
    }

    [HttpPut("{teamId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Update(Guid teamId, UpdateTeamRequest request)
    {
        var command = _mapper.Map<UpdateTeamCommand>((teamId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [HttpDelete("{teamId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Delete(Guid teamId)
    {
        var command = new DeleteTeamCommand(teamId);
        await _mediatr.Send(command);

        return NoContent();
    }
}
