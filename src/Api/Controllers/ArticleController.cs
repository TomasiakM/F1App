using Application.Dtos.Article.Requests;
using Application.Dtos.Common;
using Application.Features.Articles.Commands.Create;
using Application.Features.Articles.Commands.Like;
using Application.Features.Articles.Commands.Update;
using Application.Features.Articles.Queries.GetArticle;
using Application.Features.Articles.Queries.GetPaginated;
using Application.Features.Articles.Queries.GetPaginatedAdmin;
using Domain.Aggregates.Roles;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/article")]
public sealed class ArticleController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ArticleController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationFilters filters)
    {
        var query = new GetPaginatedArticlesQuery(filters);
        var res = await _sender.Send(query);

        return Ok(res);
    }

    [HttpGet("admin")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> GetAdmin([FromQuery] PaginationFilters filters)
    {
        var query = new GetPaginatedAdminArticlesQuery(filters);
        var res = await _sender.Send(query);

        return Ok(res);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var query = new GetArticleQuery(slug);
        var res = await _sender.Send(query);

        return Ok(res);
    }


    [HttpPost]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Create(CreateArticleRequest request)
    {
        var command = _mapper.Map<CreateArticleCommand>(request);
        await _sender.Send(command);

        return Ok();
    }

    [HttpPut("{articleId}")]
    [Authorize(Roles = Role.AdminRoleName)]
    public async Task<IActionResult> Update(Guid articleId, UpdateArticleRequest request)
    {
        var command = _mapper.Map<UpdateArticleCommand>((articleId, request));
        await _sender.Send(command);

        return Ok();
    }

    [Authorize]
    [HttpPost("{articleId}/like")]
    public async Task<IActionResult> LikeArticle(Guid articleId)
    {
        var command = new LikeArticleCommand(articleId);
        await _sender.Send(command);

        return Ok();
    }
}