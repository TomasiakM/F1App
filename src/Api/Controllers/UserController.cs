using Application.Dtos.Auth.Requests;
using Application.Dtos.User.Requests;
using Application.Features.User.Commands;
using Application.Features.User.Commands.Register;
using Application.Features.User.Commands.UpdatePassword;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/user")]
public sealed class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        await _sender.Send(command);

        return Ok();
    }

    [HttpPost("password")]
    [Authorize]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
    {
        var command = _mapper.Map<UpdatePasswordCommand>(request);
        await _sender.Send(command);

        return Ok();
    }

    [HttpPost("{userId}/ban")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Ban(BanUserRequest request, Guid userId)
    {
        var command = _mapper.Map<BanUserCommand>((userId, request));
        await _sender.Send(command);

        return Ok();
    }
}
