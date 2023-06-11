using Application.Dtos.Comments.Requests;
using Application.Features.Comments.Commands.AddReply;
using Application.Features.Comments.Commands.Create;
using Application.Features.Comments.Queries.GetAllByArticle;
using MapsterMapper;
using MediatR;
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

    [HttpPost("{articleId}")]
    public async Task<IActionResult> Create(Guid articleId, CreateCommentRequest request)
    {
        var command = _mapper.Map<CreateCommentCommand>((articleId, request));
        await _sender.Send(command);

        return Ok();
    }

    [HttpPost("{commentId}/reply")]
    public async Task<IActionResult> AddReply(Guid commentId, AddReplyRequest request)
    {
        var command = _mapper.Map<AddReplyCommand>((commentId, request));
        await _sender.Send(command);

        return Ok();
    }
}
