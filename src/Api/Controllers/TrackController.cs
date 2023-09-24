using Application.Dtos.Common;
using Application.Dtos.Track.Requests;
using Application.Features.Tracks.Commands.Create;
using Application.Features.Tracks.Commands.Delete;
using Application.Features.Tracks.Commands.Update;
using Application.Features.Tracks.Queries.Get;
using Application.Features.Tracks.Queries.GetAll;
using Application.Features.Tracks.Queries.GetPaginated;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/track")]
public sealed class TrackController : ControllerBase
{
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public TrackController(ISender mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [HttpGet("{trackSlug}")]
    public async Task<IActionResult> GetBySlug(string trackSlug)
    {
        var query = new GetTrackQuery(trackSlug);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] PaginationFilters filters)
    {
        var query = new GetPaginatedTracksQuery(filters);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllTracksQuery();
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Create(CreateTrackRequest request)
    {
        var command = _mapper.Map<CreateTrackCommand>(request);
        await _mediatr.Send(command);

        return Ok();
    }

    [HttpPut("{trackId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Update(Guid trackId, UpdateTrackRequest request)
    {
        var command = _mapper.Map<UpdateTrackCommand>((trackId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [HttpDelete("{trackId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Delete(Guid trackId)
    {
        var command = new DeleteTrackCommand(trackId);
        await _mediatr.Send(command);

        return NoContent();
    }
}
