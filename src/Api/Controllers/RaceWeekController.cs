using Application.Dtos.RaceWeek.Requests;
using Application.Features.RaceWeeks.Commands.Create;
using Application.Features.RaceWeeks.Queries.GetBySeason;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/raceweek")]
public class RaceWeekController : ControllerBase
{
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public RaceWeekController(ISender mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [HttpGet("{seasonId}")]
    public async Task<IActionResult> GetBySeason(Guid seasonId)
    {
        var query = new GetBySeasonRaceWeeksQuery(seasonId);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRaceWeekRequest request)
    {
        var command = _mapper.Map<CreateRaceWeekCommand>(request);
        await _mediatr.Send(command);

        return Ok();
    }
}
