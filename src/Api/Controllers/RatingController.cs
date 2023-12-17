using Application.Dtos.Rating.Requests;
using Application.Features.Ratings.Commands.AddRatings;
using Application.Features.Ratings.Commands.Create;
using Application.Features.Ratings.Commands.Summarize;
using Application.Features.Ratings.Queries.Get;
using Application.Features.Ratings.Queries.GetActive;
using Application.Features.Ratings.Queries.GetUser;
using Application.Features.Ratings.Queries.IsReadyToStart;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/rating")]
public sealed class RatingController : ControllerBase
{
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public RatingController(ISender mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetActive()
    {
        var query = new GetActiveRatingQuery();
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("{ratingId}")]
    [Authorize(Roles = Role.UserRoleName)]
    public async Task<IActionResult> GetUserDriverRatings(Guid ratingId)
    {
        var query = new GetUserRatingsQuery(ratingId);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("{raceWeekId}/summary")]
    public async Task<IActionResult> GetRating(Guid raceWeekId)
    {
        var query = new GetRatingQuery(raceWeekId);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("{raceWeekId}/isReady")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> IsReadyStart(Guid raceWeekId)
    {
        var query = new IsReadyRatingQuery(raceWeekId);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpPost("{raceWeekId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Create(Guid raceWeekId)
    {
        var command = new CreateRatingCommand(raceWeekId);
        await _mediatr.Send(command);

        return Ok();
    }

    [HttpPost("{ratingId}/summarize")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Summarize(Guid ratingId)
    {
        var command = new SummarizeRatingCommand(ratingId);
        await _mediatr.Send(command);

        return Ok();
    }

    [HttpPost("{ratingId}/rate")]
    [Authorize(Roles = Role.UserRoleName)]
    public async Task<IActionResult> AddRatings(Guid ratingId, AddRatingsRequest request)
    {
        var command = _mapper.Map<AddRatingsCommand>((ratingId, request));
        await _mediatr.Send(command);

        return Ok();
    }
}
