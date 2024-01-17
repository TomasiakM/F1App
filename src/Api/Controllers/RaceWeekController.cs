using Application.Dtos.RaceWeek.Requests;
using Application.Features.RaceWeeks.Commands.Create;
using Application.Features.RaceWeeks.Commands.Delete;
using Application.Features.RaceWeeks.Commands.Update;
using Application.Features.RaceWeeks.Commands.UpdateFP1SessionResults;
using Application.Features.RaceWeeks.Commands.UpdateFP2SessionResults;
using Application.Features.RaceWeeks.Commands.UpdateFP3SessionResults;
using Application.Features.RaceWeeks.Commands.UpdateRaceQualificationSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateRaceSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateSprintQualificationSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateSprintSessionResults;
using Application.Features.RaceWeeks.Queries.Get;
using Application.Features.RaceWeeks.Queries.GetBySeason;
using Application.Features.RaceWeeks.Queries.GetByTrack;
using Application.Features.RaceWeeks.Queries.GetNext;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{year}/{slug}")]
    public async Task<IActionResult> Get(int year, string slug)
    {
        var query = new GetRaceWeekQuery(year, slug);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("{seasonId}")]
    public async Task<IActionResult> GetBySeason(Guid seasonId)
    {
        var query = new GetBySeasonRaceWeeksQuery(seasonId);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("track/{trackId}")]
    public async Task<IActionResult> GetByTrack(Guid trackId)
    {
        var query = new GetByTrackRaceWeeksQuery(trackId);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("next")]
    public async Task<IActionResult> GetNext()
    {
        var query = new GetNextRaceWeekQuery();
        var response = await _mediatr.Send(query);

        return Ok(response);
    }


    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateRaceWeekRequest request)
    {
        var command = _mapper.Map<CreateRaceWeekCommand>(request);
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPut("{raceWeekId}")]
    public async Task<IActionResult> Update(Guid raceWeekId, UpdateRaceWeekRequest request)
    {
        var command = _mapper.Map<UpdateRaceWeekCommand>((raceWeekId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpDelete("{raceWeekId}")]
    public async Task<IActionResult> Update(Guid raceWeekId)
    {
        var command = new DeleteRaceWeekCommand(raceWeekId);
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPut("{raceWeekId}/fp1")]
    public async Task<IActionResult> UpdateFp1SessionResults(Guid raceWeekId, UpdateFreePracticeSessionResultsRequest request)
    {
        var command = _mapper.Map<UpdateFP1SessionResultsCommand>((raceWeekId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPut("{raceWeekId}/fp2")]
    public async Task<IActionResult> UpdateFp2SessionResults(Guid raceWeekId, UpdateFreePracticeSessionResultsRequest request)
    {
        var command = _mapper.Map<UpdateFP2SessionResultsCommand>((raceWeekId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPut("{raceWeekId}/fp3")]
    public async Task<IActionResult> UpdateFp3SessionResults(Guid raceWeekId, UpdateFreePracticeSessionResultsRequest request)
    {
        var command = _mapper.Map<UpdateFP3SessionResultsCommand>((raceWeekId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPut("{raceWeekId}/sprintqualification")]
    public async Task<IActionResult> UpdateSprintQualificationSessionResults(Guid raceWeekId, UpdateQualificationSessionResultsRequest request)
    {
        var command = _mapper.Map<UpdateSprintQualificationSessionResultsCommand>((raceWeekId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPut("{raceWeekId}/sprint")]
    public async Task<IActionResult> UpdateSprintSessionResults(Guid raceWeekId, UpdateRaceSessionResultsRequest request)
    {
        var command = _mapper.Map<UpdateSprintSessionResultsCommand>((raceWeekId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPut("{raceWeekId}/racequalification")]
    public async Task<IActionResult> UpdateRaceQualificationSessionResults(Guid raceWeekId, UpdateQualificationSessionResultsRequest request)
    {
        var command = _mapper.Map<UpdateRaceQualificationSessionResultsCommand>((raceWeekId, request));
        await _mediatr.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpPut("{raceWeekId}/race")]
    public async Task<IActionResult> UpdateRaceSessionResults(Guid raceWeekId, UpdateRaceSessionResultsRequest request)
    {
        var command = _mapper.Map<UpdateRaceSessionResultsCommand>((raceWeekId, request));
        await _mediatr.Send(command);

        return Ok();
    }
}
