using Application.Dtos.Auth.Requests;
using Application.Features.Auth.Queries.Login;
using Application.Features.Auth.Queries.Logout;
using Application.Features.Auth.Queries.Refresh;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AuthController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpGet("refresh")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Refresh()
    {
        var query = new RefreshQuery();
        var response = await _sender.Send(query);

        return Ok(response);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        var query = new LogoutQuery();
        await _sender.Send(query);

        return Ok();
    }
}
