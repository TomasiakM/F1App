using Application.Dtos.User.Responses;

namespace Application.Dtos.Comments.Responses;
public record CommentResponse(
    string Id,
    string Text,
    DateTimeOffset CreatedAt,
    UserResponse CreatedBy,
    List<ReplyResponse> Replies);
