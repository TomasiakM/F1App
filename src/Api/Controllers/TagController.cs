using Application.Dtos.Tag.Requests;
using Application.Features.Tags.Commands.Delete;
using Application.Features.Tags.Queries.GetAll;
using Application.Features.Tags.Commands.Create;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos.Common;
using Application.Features.Tags.Queries.GetPaginated;

namespace Api.Controllers;

[ApiController]
[Route("api/tag")]
public sealed class TagController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TagController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllTagsQuery();
        var res = await _sender.Send(query);

        return Ok(res);
    }

    [HttpGet("admin")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> GetPaginated([FromQuery] PaginationFilters filtes)
    {
        var query = new GetPaginatedTagsQuery(filtes);
        var res = await _sender.Send(query);

        return Ok(res);
    }

    [HttpPost]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Create(CreateTagRequest request)
    {
        var command = _mapper.Map<CreateTagCommand>(request);
        await _sender.Send(command);

        return Ok();
    }

    [HttpDelete("{tagId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Delete(Guid tagId)
    {
        var command = new DeleteTagCommand(tagId);
        await _sender.Send(command);

        return NoContent();
    }
}
