using Application.Dtos.Comments.Requests;
using Application.Features.Comments.Commands.AddReply;
using Application.Features.Comments.Commands.Create;
using Application.Features.Comments.Commands.Delete;
using Application.Features.Comments.Commands.DeleteReply;
using Application.Features.Comments.Queries.GetAllByArticle;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/comment")]
public sealed class CommentController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CommentController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("{articleId}")]
    public async Task<IActionResult> GetAllByarticle(Guid articleId)
    {
        var query = new GetAllCommentsByArticleQuery(articleId);
        var response = await _sender.Send(query);

        return Ok(response);
    }

    [Authorize]
    [HttpPost("{articleId}")]
    public async Task<IActionResult> Create(Guid articleId, CreateCommentRequest request)
    {
        var command = _mapper.Map<CreateCommentCommand>((articleId, request));
        await _sender.Send(command);

        return Ok();
    }

    [Authorize]
    [HttpPost("{commentId}/reply")]
    public async Task<IActionResult> AddReply(Guid commentId, AddReplyRequest request)
    {
        var command = _mapper.Map<AddReplyCommand>((commentId, request));
        await _sender.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var command = new DeleteCommentCommand(commentId);
        await _sender.Send(command);

        return Ok();
    }

    [Authorize(Roles = Role.AdminRoleName)]
    [HttpDelete("{commentId}/reply/{replyId}")]
    public async Task<IActionResult> DeleteReply(Guid commentId, Guid replyId)
    {
        var command = new DeleteReplyCommand(commentId, replyId);
        await _sender.Send(command);

        return Ok();
    }
}
