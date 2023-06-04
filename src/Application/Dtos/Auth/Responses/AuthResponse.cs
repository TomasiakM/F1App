namespace Application.Dtos.Auth.Responses;
public record AuthResponse(
    string Id,
    string Username,
    string Image,
    List<string> Roles,
    string AccessToken);
