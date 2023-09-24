using Application.Dtos.Season.Requests;
using Application.Features.Seasons.Commands.Create;
using Application.Features.Seasons.Commands.Delete;
using Application.Features.Seasons.Queries.GetAll;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/season")]
public sealed class SeasonController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public SeasonController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllSeasonsQuery();
        var res = await _sender.Send(query);

        return Ok(res);
    }

    [HttpPost]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Create(CreateSeasonRequest request)
    {
        var query = _mapper.Map<CreateSeasonCommand>(request);
        await _sender.Send(query);

        return Ok();
    }

    [HttpDelete("{seasonId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Create(Guid seasonId)
    {
        var query = new DeleteSeasonCommand(seasonId);
        await _sender.Send(query);

        return NoContent();
    }
}
