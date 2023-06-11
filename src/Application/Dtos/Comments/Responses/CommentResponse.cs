using Application.Dtos.User.Responses;

namespace Application.Dtos.Comments.Responses;
public record CommentResponse(
    string Id,
    string Text,
    string CreatedAt,
    UserResponse User,
    List<ReplyResponse> Replies);
