using Application.Features.GeneralClassifications.Queries.GetDrivers;
using Application.Features.GeneralClassifications.Queries.GetTeams;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/classification")]
public class GeneralClassificationController : ControllerBase
{
    private readonly ISender _mediatr;

    public GeneralClassificationController(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("{year}/driver")]
    public async Task<IActionResult> GetDriverClassification(int year)
    {
        var query = new GetDriverGeneralClassificationQuery(year);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }

    [HttpGet("{year}/team")]
    public async Task<IActionResult> GetTeamClassification(int year)
    {
        var query = new GetTeamGeneralClassificationQuery(year);
        var response = await _mediatr.Send(query);

        return Ok(response);
    }
}
