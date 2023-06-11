using Application.Dtos.Comments.Responses;
using MediatR;

namespace Application.Features.Comments.Queries.GetAllByArticle;
public record GetAllCommentsByArticleQuery(
    Guid ArticleId) : IRequest<List<CommentResponse>>;
