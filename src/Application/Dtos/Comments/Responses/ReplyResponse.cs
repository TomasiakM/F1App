using Application.Dtos.User.Responses;

namespace Application.Dtos.Comments.Responses;
public record ReplyResponse(
    string Id,
    string Text,
    DateTimeOffset CreatedAt,
    UserResponse CreatedBy);
