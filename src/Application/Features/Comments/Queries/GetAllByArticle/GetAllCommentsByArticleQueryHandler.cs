using Application.Dtos.Comments.Responses;
using Application.Interfaces;
using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Users.ValueObjects;
using MapsterMapper;
using MediatR;

namespace Application.Features.Comments.Queries.GetAllByArticle;
internal sealed class GetAllCommentsByArticleQueryHandler : IRequestHandler<GetAllCommentsByArticleQuery, List<CommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCommentsByArticleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CommentResponse>> Handle(GetAllCommentsByArticleQuery request, CancellationToken cancellationToken)
    {
        var articleId = ArticleId.Create(request.ArticleId);
        var comments = await _unitOfWork.Comments.FindAllAsync(e => e.ArticleId == articleId);
        
        return await MapComments(comments);
    }

    private async Task<List<CommentResponse>> MapComments(ICollection<Comment> comments)
    {
        List<UserId> userIds = new();
        foreach(var comment in comments)
        {
            userIds.Add(comment.CreatedBy);
            userIds.AddRange(comment.Replies.Select(e => e.CreatedBy));
        }
        userIds = userIds.Distinct().ToList();

        var users = await _unitOfWork.Users.FindAllAsync(e => userIds.Contains(e.Id));

        List<CommentResponse> commentDtos = new();
        foreach (var comment in comments)
        {
            List<ReplyResponse> replyDtos = new();
            foreach(var reply in comment.Replies)
            {
                var replyDto = _mapper.Map<ReplyResponse>((reply, users.FirstOrDefault(e => e.Id == reply.CreatedBy)));
                replyDtos.Add(replyDto);
            }

            var commentDto = _mapper.Map<CommentResponse>((comment, replyDtos, users.FirstOrDefault(e => e.Id == comment.CreatedBy)));
            commentDtos.Add(commentDto);
        }

        return commentDtos;
    }
}
