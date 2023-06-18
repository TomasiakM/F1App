using Application.Dtos.Comments.Requests;
using Application.Dtos.Comments.Responses;
using Application.Features.Comments.Commands.AddReply;
using Application.Features.Comments.Commands.Create;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Comments.Entities;
using Domain.Aggregates.Users;
using Mapster;

namespace Application.Mapper;
internal sealed class CommentMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid articleId, CreateCommentRequest request), CreateCommentCommand>()
            .Map(dest => dest.ArticleId, src => src.articleId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(Guid commentId, AddReplyRequest request), AddReplyCommand>()
            .Map(dest => dest.CommentId, src => src.commentId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(Comment comment, List<ReplyResponse> replies, User user), CommentResponse>()
            .Map(dest => dest.Id, src => src.comment.Id.Value)
            .Map(dest => dest.Replies, src => src.replies)
            .Map(dest => dest.CreatedBy, src => src.user)
            .Map(dest => dest, src => src.comment);

        config.NewConfig<(Reply reply, User user), ReplyResponse>()
            .Map(dest => dest.Id, src => src.reply.Id.Value)
            .Map(dest => dest.CreatedBy, src => src.user)
            .Map(dest => dest, src => src.reply);
    }
}
